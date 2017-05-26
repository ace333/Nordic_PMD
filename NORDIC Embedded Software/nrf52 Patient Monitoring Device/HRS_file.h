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
#include "app_util_platform.h"
#include "app_error.h"
#include "nrf_delay.h"
#define NRF_LOG_MODULE_NAME "APP"
#include "nrf_log.h"
#include "nrf_drv_twi.h"


/* TWI instance ID. */
#define MAX30105_INSTANCE_ID     0

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
static const nrf_drv_twi_t MAX30105_twi = NRF_DRV_TWI_INSTANCE(MAX30105_INSTANCE_ID);

void MAX30105_config_parameter(uint8_t REGISTER, uint8_t value);

void MAX30105_default_HRS_config(void);

void MAX30105_set_pointer(uint8_t REGISTER);

uint8_t MAX30105_Get_Byte(uint8_t ADRESS, uint8_t REGISTER);

void MAX30105_twi_init (void);

uint32_t MAX30105_Get_Sample (void);
