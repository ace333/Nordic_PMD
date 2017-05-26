/* Copyright (c) 2017, Bartlomiej Cerek, Arkadiusz Chudy.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer 
in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote 
products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

#include <stdlib.h>
#include <stdio.h>
#include "HRS_file.h" 

void MAX30105_config_parameter(uint8_t REGISTER, uint8_t value)
{
	  uint8_t reg[2] = {REGISTER, value};
    ret_code_t err_code = nrf_drv_twi_tx(&MAX30105_twi, MAX30105_ADD, reg, sizeof(reg), false);
		//printf("%d",err_code) ;
    APP_ERROR_CHECK(err_code);
}

void MAX30105_default_HRS_config(void)
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
    err_code = nrf_drv_twi_tx(&MAX30105_twi, MAX30105_ADD, reg, 1, false);
    APP_ERROR_CHECK(err_code);
}


uint8_t MAX30105_Get_Byte(uint8_t ADRESS, uint8_t REGISTER)
{
	MAX30105_set_pointer(REGISTER);
	uint8_t data;
	ret_code_t err_code ;
	err_code = nrf_drv_twi_rx(&MAX30105_twi, ADRESS, &data, 1);
  APP_ERROR_CHECK(err_code);
	return data;
}

void MAX30105_twi_init (void)
{
    ret_code_t err_code;

    const nrf_drv_twi_config_t twi_lm75b_config = {
       .scl                = 22,
       .sda                = 23,
       .frequency          = NRF_TWI_FREQ_100K,
       .interrupt_priority = APP_IRQ_PRIORITY_HIGH,
    };

    err_code = nrf_drv_twi_init(&MAX30105_twi, &twi_lm75b_config , NULL, NULL);
    APP_ERROR_CHECK(err_code);

    nrf_drv_twi_enable(&MAX30105_twi);
}

uint32_t MAX30105_Get_Sample (void)
{	
	
	//define variables
		uint8_t IR[4] ;
		uint8_t wrt_ptr;
		uint8_t read_ptr;
		int no_samples;
		
		uint8_t * data;
		data	= malloc(sizeof(uint8_t)*6) ; //defaultowy bufor na 6 probek
		uint32_t * tab_samples ;
		
		uint32_t IR_value = 0 ;
	
		wrt_ptr = MAX30105_Get_Byte(MAX30105_ADD, MAX30105_FIFO_WRITE);
		read_ptr = MAX30105_Get_Byte(MAX30105_ADD, MAX30105_FIFO_READ);
		no_samples = wrt_ptr - read_ptr;
		if (no_samples < 0) no_samples += 32 ; // wrap condition
			
		tab_samples = malloc(sizeof(uint32_t)*no_samples)	;
			
		for(int i =0 ; i<no_samples ; i++)
			{			
			MAX30105_set_pointer(MAX30105_FIFO_DATA);
				
			nrf_drv_twi_rx(&MAX30105_twi, MAX30105_ADD, data, 6);
			
			//data from 0 - 2 indexes is red and we don't care	
			IR[3] = 0 ;	
			IR[2] = data[3];
			IR[1] = data[4];
			IR[0] = data[5];	
					
			IR_value = 65536*IR[2] + 256*IR[1] + IR[0] ;	
			tab_samples[i] = IR_value ;
			// debug NRF_LOG_INFO("%d , %d , %d \r\n",IR[1],IR[2],IR[3]);
			// unia - sumowanie bajtow
			}	
			
			IR_value = tab_samples[no_samples-1] ;
			free(tab_samples) ;
			free(data) ;
			
		return IR_value ;		
		
}
