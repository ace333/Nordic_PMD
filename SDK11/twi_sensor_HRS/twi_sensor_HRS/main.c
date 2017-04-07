/* Copyright (c) 2015 Nordic Semiconductor. All Rights Reserved.
 *
 * The information contained herein is property of Nordic Semiconductor ASA.
 * Terms and conditions of usage are described in detail in NORDIC
 * SEMICONDUCTOR STANDARD SOFTWARE LICENSE AGREEMENT.
 *
 * Licensees are granted free, non-transferable use of the information. NO
 * WARRANTY of ANY KIND is provided. This heading must NOT be removed from
 * the file.
 *
 */

#include <stdlib.h>
#include <stdio.h>
#include "boards.h"
#include "app_util_platform.h"
#include "app_error.h"
#include "nrf_drv_twi.h"
#include "nrf_delay.h"
#define NRF_LOG_MODULE_NAME "APP"
#include "nrf_log.h"
//#include "nrf_log_ctrl.h"

/* TWI instance ID. */
#define TWI_INSTANCE_ID     0

/* Common addresses definition for sensor. */

#define MAX30105_ADD 0x57

#define MAX30105_FIFO_CONFIG 0x08
#define MAX30105_MODE_CONFIG 0x09
#define MAX30105_PARTICLE_SENSING_CONFIG 0x0A
#define MAX30105_LED1 0x0C
#define MAX30105_LED2 0x0D
#define MAX30105_LED3 0x0E
#define MAX30105_PROXIMITY 0x10
#define MAX30105_SLOT1 0x11
#define MAX30105_SLOT2 0x12

#define MAX30105_FIFO_WRITE 0x04
#define MAX30105_FIFO_READ 0x06
#define MAX30105_FIFO_DATA 0x07

#define NRF_LOG_USES_UART 1
#define ENABLE_DEBUG_LOG_SUPPORT 1


/* Indicates if operation on TWI has ended. */
static volatile bool m_xfer_done = false;

/* TWI instance. */
static const nrf_drv_twi_t m_twi = NRF_DRV_TWI_INSTANCE(TWI_INSTANCE_ID);

void MAX30105_config_parameter(uint8_t REGISTER, uint8_t value)
{
	  uint8_t reg[2] = {REGISTER, value};
    ret_code_t err_code = nrf_drv_twi_tx(&m_twi, MAX30105_ADD, reg, sizeof(reg), false);
    APP_ERROR_CHECK(err_code);
		
		nrf_delay_ms(200);
}

void MAX30105_default_HRS_config()
{
	  MAX30105_config_parameter(MAX30105_FIFO_CONFIG, 112); // avreaging of 8 samples, enables rollover
		MAX30105_config_parameter(MAX30105_MODE_CONFIG, 7); // ALL
	  MAX30105_config_parameter(MAX30105_PARTICLE_SENSING_CONFIG, 39); // ADC:4096 SampleRate: 100 PulseWinteidith: 411
		MAX30105_config_parameter(MAX30105_LED1, 31); //setting power on LEDs
		MAX30105_config_parameter(MAX30105_LED2, 31);
		MAX30105_config_parameter(MAX30105_LED3, 31);
		MAX30105_config_parameter(MAX30105_PROXIMITY, 31);
		MAX30105_config_parameter(MAX30105_SLOT1, 18);//Enable slots 1 and 2
		MAX30105_config_parameter(MAX30105_SLOT2, 0);//Disable slots 3 and 4
}

 
void MAX30105_set_pointer(uint8_t REGISTER)
{
    ret_code_t err_code;
    uint8_t reg[1];
  
    /* Writing to pointer byte. */
    reg[0] = REGISTER;
    err_code = nrf_drv_twi_tx(&m_twi, MAX30105_ADD, reg, 1, false);
    APP_ERROR_CHECK(err_code);
}


uint8_t TWI_Get_Byte(uint8_t ADRESS, uint8_t REGISTER)
{
	MAX30105_set_pointer(REGISTER);
	uint8_t data;
	ret_code_t err_code ;
	err_code = nrf_drv_twi_rx(&m_twi, ADRESS, &data, 1);
  APP_ERROR_CHECK(err_code);
	return data;
}

void twi_init (void)
{
    ret_code_t err_code;

    const nrf_drv_twi_config_t twi_lm75b_config = {
       .scl                = 26,
       .sda                = 2,
       .frequency          = NRF_TWI_FREQ_100K,
       .interrupt_priority = APP_IRQ_PRIORITY_HIGH,
    };

    err_code = nrf_drv_twi_init(&m_twi, &twi_lm75b_config , NULL, NULL);
    APP_ERROR_CHECK(err_code);

    nrf_drv_twi_enable(&m_twi);
}

uint32_t Get_Last_IR_Sample ()
{	
	//define variables
		uint8_t IR[4] ;
		uint8_t wrt_ptr;
		uint8_t read_ptr;
		uint8_t no_samples;
		
		uint8_t * data;
		data	= malloc(sizeof(uint8_t)*6) ; //defaultowy bufor na 6 probek
		uint32_t * tab_samples ;
		
		uint32_t IR_value = 0 ;
	
		wrt_ptr = TWI_Get_Byte(MAX30105_ADD, MAX30105_FIFO_WRITE);
		read_ptr = TWI_Get_Byte(MAX30105_ADD, MAX30105_FIFO_READ);
		no_samples = wrt_ptr - read_ptr;
		if (no_samples < 0) no_samples += 32 ; // wrap condition
			
		tab_samples = malloc(sizeof(uint32_t)*no_samples)	;
			
		for(int i =0 ; i<no_samples ; i++)
			{			
			MAX30105_set_pointer(MAX30105_FIFO_DATA);
				
			nrf_drv_twi_rx(&m_twi, MAX30105_ADD, data, 6);
			
			//data from 0 - 2 indexes is red and we don't care	
			IR[3] = 0 ;	
			IR[2] = data[3];
			IR[1] = data[4];
			IR[0] = data[5];	
					
			memcpy(&IR_value , IR, 4);	
			tab_samples[i] = IR_value ;
			// debug NRF_LOG_INFO("%d , %d , %d \r\n",IR[1],IR[2],IR[3]);
			}	
			
			IR_value = tab_samples[no_samples-1] ;
			free(tab_samples) ;
			
		return IR_value ;
}


int main(void)
{
    //APP_ERROR_CHECK(NRF_LOG_INIT(NULL));
		NRF_LOG_INIT() ;
	
    NRF_LOG_PRINTF("Read from MAX30105\r\n");
 
    twi_init();
	  NRF_LOG_PRINTF(" TWI initialized.\r\n");
	
		MAX30105_default_HRS_config();
		NRF_LOG_PRINTF("Configuration done\r\n");
	
    uint32_t IR_value ;
				
    while (true)
    {
			
			IR_value =  Get_Last_IR_Sample () ;
			NRF_LOG_PRINTF("%d",IR_value);
			
			nrf_delay_ms(500);
    }
}

/** @} */
