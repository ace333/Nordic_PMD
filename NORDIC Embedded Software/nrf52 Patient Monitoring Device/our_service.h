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
 
#ifndef OUR_SERVICE_H__
#define OUR_SERVICE_H__

#include <stdint.h>
#include "ble.h"
#include "ble_srv_common.h"

// FROM_SERVICE_TUTORIAL: Defining 16-bit service and 128-bit base UUIDs
#define BLE_UUID_OUR_BASE_UUID              {{0x23, 0xD1, 0x13, 0xEF, 0x5F, 0x78, 0x23, 0x15, 0xDE, 0xEF, 0x12, 0x12, 0x00, 0x00, 0x00, 0x00}} // 128-bit base UUID
#define BLE_UUID_OUR_ACC_UUID                0xACC0 // acc service
#define BLE_UUID_OUR_HRS_UUID								 0xEE00 //heart rate service

// ALREADY_DONE_FOR_YOU: Defining 16-bit characteristic UUID
#define BLE_UUID_ACC_X          0x1ACC // X
#define BLE_UUID_ACC_Y          0x2ACC // Y
#define BLE_UUID_ACC_Z          0x3ACC // Z
#define BLE_UUID_HRS          0xEE10 // Z

// This structure contains various status information for our service. 
// The name is based on the naming convention used in Nordics SDKs. 
// 'ble� indicates that it is a Bluetooth Low Energy relevant structure and 
// �os� is short for Our Service). 
typedef struct
{
    uint16_t                    conn_handle;    /**< Handle of the current connection (as provided by the BLE stack, is BLE_CONN_HANDLE_INVALID if not in a connection).*/
    uint16_t                    service_handle; /**< Handle of Our Service (as provided by the BLE stack). */
    // OUR_JOB: Step 2.D, Add handles for the characteristic attributes to our struct
    ble_gatts_char_handles_t    char_handles_acc1;
		ble_gatts_char_handles_t    char_handles_acc2;
		ble_gatts_char_handles_t    char_handles_acc3;
		ble_gatts_char_handles_t    char_handles_hrs;
}ble_os_t;

/**@brief Function for handling BLE Stack events related to our service and characteristic.
 *
 * @details Handles all events from the BLE stack of interest to Our Service.
 *
 * @param[in]   p_our_service       Our Service structure.
 * @param[in]   p_ble_evt  Event received from the BLE stack.
 */
void ble_our_service_on_ble_evt(ble_os_t * p_our_service, ble_evt_t * p_ble_evt);

/**@brief Function for initializing our new service.
 *
 * @param[in]   p_our_service       Pointer to Our Service structure.
 */
void acc_service_init(ble_os_t * p_our_service);
void hrs_service_init(ble_os_t * p_our_service);

/**@brief Function for updating and sending new characteristic values
 *
 * @details The application calls this function whenever our timer_timeout_handler triggers
 *
 * @param[in]   p_our_service                     Our Service structure.
 * @param[in]   characteristic_value     New characteristic value.
 */
void acc1_value_update(ble_os_t *p_our_service, int32_t *value);
void acc2_value_update(ble_os_t *p_our_service, int32_t *value);
void acc3_value_update(ble_os_t *p_our_service, int32_t *value);
void hrs_value_update(ble_os_t *p_our_service, uint32_t *value);
#endif  /* _ OUR_SERVICE_H__ */
