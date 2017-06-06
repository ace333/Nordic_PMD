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
 
#include <stdio.h>
#include "boards.h"
#include "app_util_platform.h"
#include "app_error.h"
#include "nrf_drv_twi.h"
#include "nrf_delay.h"
#define NRF_LOG_MODULE_NAME "APP"
#include "nrf_log.h"

/* TWI instance ID. */
#define LSM303D_INSTANCE_ID     1

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

/* TWI instance. */
static const nrf_drv_twi_t LSM303D_twi = NRF_DRV_TWI_INSTANCE(LSM303D_INSTANCE_ID);

void LSM303D_set_mode(uint8_t REGISTER);

uint8_t LSM303D_TWI_Get_Byte(uint8_t ADRESS, uint8_t REGISTER);

void LSM303D_twi_init (void) ;
	
int LSM303D_Get_Temperature(void) ;

int16_t LSM303D_Get_ACC(uint8_t ADD_HIGH, uint8_t ADD_LOW);
	
int16_t LSM303D_Get_X(void) ;
int16_t LSM303D_Get_Y(void) ;
int16_t LSM303D_Get_Z(void) ;

void LSM303D_Set_Default_Mode(void) ;
