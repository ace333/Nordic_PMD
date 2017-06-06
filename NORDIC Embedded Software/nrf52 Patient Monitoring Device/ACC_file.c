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
LOSS OF USE, DATA, OR PROFITS;
OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/


#include "ACC_file.h"
#include "config.h"

void LSM303D_set_mode(uint8_t register_num)
{
    ret_code_t err_code;
    uint8_t reg = register_num;
    err_code = nrf_drv_twi_tx(&LSM303D_twi, LSM303D_ADDR, &reg, 1, false);
    APP_ERROR_CHECK(err_code);
}
uint8_t LSM303D_TWI_Get_Byte(uint8_t address, uint8_t register_num)
{
    LSM303D_set_mode(register_num);
    uint8_t data;
    ret_code_t err_code ;
    err_code = nrf_drv_twi_rx(&LSM303D_twi, address, &data, 1);
    APP_ERROR_CHECK(err_code);
    return data;
}
void LSM303D_twi_init (void)
{
    ret_code_t err_code;
    const nrf_drv_twi_config_t twi_LSM303D_config = {
        .scl = LSM303D_SCL,
        .sda = LSM303D_SDA,
        .frequency  = NRF_TWI_FREQ_100K,
        .interrupt_priority = APP_IRQ_PRIORITY_HIGH,
    };
    err_code = nrf_drv_twi_init(&LSM303D_twi, &twi_LSM303D_config , NULL, NULL);
    APP_ERROR_CHECK(err_code);
    nrf_drv_twi_enable(&LSM303D_twi);
}
int LSM303D_Get_Temperature()
{
    //Tej funkcji mozna uzywac dopiero po wczesniejszym wlaczeniu pomiaru temperatury
    uint8_t low = LSM303D_TWI_Get_Byte(LSM303D_ADDR, LSM303D_TEMP_OUT_L);
    uint8_t high = LSM303D_TWI_Get_Byte(LSM303D_ADDR, LSM303D_TEMP_OUT_H);
    int16_t x = 0 ;
    x = ((x +high) << 8 ) + low ;
    return x;
}

int16_t LSM303D_Get_ACC(uint8_t add_high, uint8_t add_low)
{
    //Tej funkcji mozna uzywac dopiero po wczesniejszym wlaczeniu akcelerometru
    int16_t acc = 0;
    uint8_t byte_array [2] ;
	  //Pobierz dwa bajty skladajace sie na wartosc przyspieszenia
    byte_array[0] = LSM303D_TWI_Get_Byte(LSM303D_ADDR, add_low);
    byte_array[1] = LSM303D_TWI_Get_Byte(LSM303D_ADDR, add_high);
    //Konwersja
		acc = 256 * byte_array[1] + byte_array[0] ;
    return acc;
}

int16_t LSM303D_Get_X (void)
{
    return LSM303D_Get_ACC(LSM303D_OUT_X_H_A,LSM303D_OUT_X_L_A);
}
int16_t LSM303D_Get_Y (void)
{
    return LSM303D_Get_ACC(LSM303D_OUT_Y_H_A,LSM303D_OUT_Y_L_A);
}
int16_t LSM303D_Get_Z (void)
{
    return LSM303D_Get_ACC(LSM303D_OUT_Z_H_A,LSM303D_OUT_Z_L_A);
}

void LSM303D_Set_Default_Mode(void)
{
    /*All magic numbers are register values enabling features
    mentioned in comments, they are taken from LSM303D documentation,
    because they are being used only in that function they are not 
    defined in .h file.	
    */
	
    // Turning on temperature meassurement
    uint8_t reg[2] = { LSM303D_CTRL5 , 144 }; // enable default temp. messurement
    ret_code_t err_code = nrf_drv_twi_tx(&LSM303D_twi, LSM303D_ADDR, reg, sizeof(reg), false);
    APP_ERROR_CHECK(err_code);
		
    nrf_delay_ms(100); // wait until initialization of messurment is done (advised in documentation of LSM303D)
		
    // Turning on accelerometer messurement
    reg[0] = LSM303D_CTRL1;
    reg[1] = 71 ; // enable default acc. messurement
    err_code = nrf_drv_twi_tx(&LSM303D_twi, LSM303D_ADDR, reg, sizeof(reg), false);
    APP_ERROR_CHECK(err_code);
		
    nrf_delay_ms(100);
}
