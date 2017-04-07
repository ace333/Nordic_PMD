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

#include <stdio.h>
#include "boards.h"
#include "app_util_platform.h"
#include "app_error.h"
#include "nrf_drv_twi.h"
#include "nrf_delay.h"
#define NRF_LOG_MODULE_NAME "APP"
#include "nrf_log.h"
#include "nrf_log_ctrl.h"

/* TWI instance ID. */
#define TWI_INSTANCE_ID     0

/* Common addresses definition for sensor. */

#define LSM303D_ADDR          0x1d

#define LSM303D_WHO_I_AM      0x0f
#define LSM303D_TEMP_OUT_L      0x05
#define LSM303D_TEMP_OUT_H      0x06
#define LSM303D_STATUS_M			0x07
#define LSM303D_OUT_X_L_M				0x08
#define LSM303D_OUT_X_H_M       0x09
#define LSM303D_OUT_Y_M				0x0a
#define LSM303D_OUT_Z_M				0x0c
#define LSM303D_CTRL0				0x1f
#define LSM303D_CTRL1				0x20
#define LSM303D_CTRL2				0x21
#define LSM303D_CTRL3				0x22
#define LSM303D_CTRL4				0x23
#define LSM303D_CTRL5				0x24
#define LSM303D_CTRL6				0x25
#define LSM303D_CTRL7				0x26
#define LSM303D_STATUS				0x27
#define LSM303D_OUT_X_L_A				0x28
#define LSM303D_OUT_X_H_A				0x29
#define LSM303D_OUT_Y_L_A				0x2a
#define LSM303D_OUT_Y_H_A				0x2b
#define LSM303D_OUT_Z_L_A				0x2c
#define LSM303D_OUT_Z_H_A				0x2d


/* Indicates if operation on TWI has ended. */
static volatile bool m_xfer_done = false;

/* TWI instance. */
static const nrf_drv_twi_t m_twi = NRF_DRV_TWI_INSTANCE(TWI_INSTANCE_ID);

 
void LSM303D_set_mode(uint8_t REGISTER)
{
    ret_code_t err_code;
    uint8_t reg[1];
  
    /* Writing to pointer byte. */
    reg[0] = REGISTER;
    err_code = nrf_drv_twi_tx(&m_twi, LSM303D_ADDR, reg, 1, false);
    APP_ERROR_CHECK(err_code);
}


uint8_t TWI_Get_Byte(uint8_t ADRESS, uint8_t REGISTER)
{
	LSM303D_set_mode(REGISTER);
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
       .clear_bus_init     = false
    };

    err_code = nrf_drv_twi_init(&m_twi, &twi_lm75b_config , NULL, NULL);
    APP_ERROR_CHECK(err_code);

    nrf_drv_twi_enable(&m_twi);
}

float LSM303D_Get_Temperature()
{
	//Tej funkcji mozna uzywac dopiero po wczesniejszym wlaczeniu pomiaru temperatury
	uint8_t low = TWI_Get_Byte(LSM303D_ADDR, LSM303D_TEMP_OUT_L);
	uint8_t high = TWI_Get_Byte(LSM303D_ADDR, LSM303D_TEMP_OUT_H);
	
	//sklejenie bajtow
	int16_t x = 0 ;
	x=((x+high)<<8)+low ;
	
	//Kontrola sytuacji 
	//NRF_LOG_INFO("\r\n Low:%d high: %d X: %d\r\n",low,high,x);
	//NRF_LOG_FLUSH();
	
	//konwersja na stopnie Celsjusza
	float temp = 23 + 0.125*x ;
	return temp;
}

float * LSM303D_Get_Accelerometer()
{
	uint8_t ADD_LOW ;
	uint8_t ADD_HIGH ;
	float acc[3] ;	
	int i ;
	
	for(i =0;i<3;i++)
	{
		switch(i)
		{
			case 0:
				ADD_LOW = LSM303D_OUT_X_L_A ;
				ADD_HIGH = LSM303D_OUT_X_H_A ;
				break ;
			case 1:
				ADD_LOW = LSM303D_OUT_Y_L_A ;
				ADD_HIGH = LSM303D_OUT_Y_H_A ;
				break ;
			case 2:
				ADD_LOW = LSM303D_OUT_Z_L_A ;
				ADD_HIGH = LSM303D_OUT_Z_H_A ;
				break ;
		}
	
	//Tej funkcji mozna uzywac dopiero po wczesniejszym wlaczeniu akcelerometru
	uint8_t low = TWI_Get_Byte(LSM303D_ADDR, ADD_LOW);
	uint8_t high = TWI_Get_Byte(LSM303D_ADDR, ADD_HIGH);
	
	//Konwersja bajtowa, nie uwzglednia znaku, trzeba to rozkminic	
	int16_t x = 0 ;
	x=((x+high)<<8)+low ;

	//konwersja na g
	 acc[i]= x * 2.0 / 32678.0;
	}
	return acc;
}

int main(void)
{
		//Power on (Vin)
		nrf_gpio_cfg_output(25);
		nrf_gpio_pin_toggle(25);
	
    APP_ERROR_CHECK(NRF_LOG_INIT(NULL));

    NRF_LOG_INFO("Read from LSM303D\r\n");
    NRF_LOG_FLUSH();
    twi_init();
	  NRF_LOG_INFO("Initialized.\r\n");
	  NRF_LOG_FLUSH();
		
		// Turning on temperature meassurement
    uint8_t reg[2] = {LSM303D_CTRL5	, 144};
    ret_code_t err_code = nrf_drv_twi_tx(&m_twi, LSM303D_ADDR, reg, sizeof(reg), false);
    APP_ERROR_CHECK(err_code);
		nrf_delay_ms(500);
		NRF_LOG_INFO("Temperature meassurement on.\r\n");
	  NRF_LOG_FLUSH();
		
		// Turning on accelerometer
    reg[0] = LSM303D_CTRL1;	
		reg[1] = 71;
    err_code = nrf_drv_twi_tx(&m_twi, LSM303D_ADDR, reg, sizeof(reg), false);
    APP_ERROR_CHECK(err_code);
		nrf_delay_ms(500);
		NRF_LOG_INFO("\r\n Accelerometer on.\r\n");
	  NRF_LOG_FLUSH();
		
		float temp;
		float acc_x;
		float acc_y;
		float acc_z;
				
    while (true)
    {
			temp = LSM303D_Get_Temperature();
			acc_x = LSM303D_Get_Accelerometer()[0] ;
			acc_y = LSM303D_Get_Accelerometer()[1] ;
			acc_z = LSM303D_Get_Accelerometer()[2] ;
			
			
			//te wiadomosc trzeba przerobic na jakas spojna i prosta do odczytu najlepiej zeby w jednej wiadomosci
			//(jendym log info) wszystkie potrzebne zmienne i jakis znak poczatku i konca transmisji. 
			NRF_LOG_INFO("Temperatura w Celsjuszach:" NRF_LOG_FLOAT_MARKER "\r\n", NRF_LOG_FLOAT(temp));
			NRF_LOG_INFO("X:'" NRF_LOG_FLOAT_MARKER "'\r\n", NRF_LOG_FLOAT(acc_x));
			NRF_LOG_INFO("Y:'" NRF_LOG_FLOAT_MARKER "'\r\n", NRF_LOG_FLOAT(acc_y));
			NRF_LOG_INFO("Z:'" NRF_LOG_FLOAT_MARKER "'\r\n", NRF_LOG_FLOAT(acc_z));
	    NRF_LOG_FLUSH();
			
			nrf_delay_ms(2000);
    }
}

/** @} */
