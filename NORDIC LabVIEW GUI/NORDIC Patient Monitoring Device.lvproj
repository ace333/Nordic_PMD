<?xml version='1.0' encoding='UTF-8'?>
<Project Type="Project" LVVersion="15008000">
	<Item Name="My Computer" Type="My Computer">
		<Property Name="NI.SortType" Type="Int">3</Property>
		<Property Name="server.app.propertiesEnabled" Type="Bool">true</Property>
		<Property Name="server.control.propertiesEnabled" Type="Bool">true</Property>
		<Property Name="server.tcp.enabled" Type="Bool">false</Property>
		<Property Name="server.tcp.port" Type="Int">0</Property>
		<Property Name="server.tcp.serviceName" Type="Str">My Computer/VI Server</Property>
		<Property Name="server.tcp.serviceName.default" Type="Str">My Computer/VI Server</Property>
		<Property Name="server.vi.callsEnabled" Type="Bool">true</Property>
		<Property Name="server.vi.propertiesEnabled" Type="Bool">true</Property>
		<Property Name="specify.custom.address" Type="Bool">false</Property>
		<Item Name="ctrls" Type="Folder">
			<Item Name="AccelerometerData.ctl" Type="VI" URL="../ctrls/AccelerometerData.ctl"/>
			<Item Name="DataBaseData.ctl" Type="VI" URL="../ctrls/DataBaseData.ctl"/>
			<Item Name="QData.ctl" Type="VI" URL="../ctrls/QData.ctl"/>
			<Item Name="RefsBox.ctl" Type="VI" URL="../ctrls/RefsBox.ctl"/>
			<Item Name="States.ctl" Type="VI" URL="../ctrls/States.ctl"/>
			<Item Name="Heart Monitor-50.ico" Type="Document" URL="../ctrls/Heart Monitor-50.ico"/>
		</Item>
		<Item Name="subs" Type="Folder">
			<Item Name="AccDataToRealValue.vi" Type="VI" URL="../subs/AccDataToRealValue.vi"/>
			<Item Name="ConfigWindow.vi" Type="VI" URL="../subs/ConfigWindow.vi"/>
			<Item Name="ConnectToDatabase.vi" Type="VI" URL="../subs/ConnectToDatabase.vi"/>
			<Item Name="ConversionToUINT32.vi" Type="VI" URL="../subs/ConversionToUINT32.vi"/>
			<Item Name="DatabaseFGV.vi" Type="VI" URL="../subs/DatabaseFGV.vi"/>
			<Item Name="DisconncetFromDatabase.vi" Type="VI" URL="../subs/DisconncetFromDatabase.vi"/>
			<Item Name="EnableAllNotifications.vi" Type="VI" URL="../subs/EnableAllNotifications.vi"/>
			<Item Name="SendACC.vi" Type="VI" URL="../subs/SendACC.vi"/>
			<Item Name="SendHRS.vi" Type="VI" URL="../subs/SendHRS.vi"/>
			<Item Name="CheckPosition.vi" Type="VI" URL="../subs/CheckPosition.vi"/>
		</Item>
		<Item Name="BLE context.lvclass" Type="LVClass" URL="../BLE Context/BLE context.lvclass"/>
		<Item Name="BLE Message.lvclass" Type="LVClass" URL="../BLE Message/BLE Message.lvclass"/>
		<Item Name="main.vi" Type="VI" URL="../main.vi"/>
		<Item Name="Dependencies" Type="Dependencies">
			<Item Name="vi.lib" Type="Folder">
				<Item Name="address_type.ctl" Type="VI" URL="/&lt;vilib&gt;/BLE/type_def/address_type.ctl"/>
				<Item Name="advertising_package_type.ctl" Type="VI" URL="/&lt;vilib&gt;/BLE/type_def/advertising_package_type.ctl"/>
				<Item Name="ble_attach_dongle.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_attach_dongle.vi"/>
				<Item Name="ble_check_payload_error.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_check_payload_error.vi"/>
				<Item Name="ble_cmd_attclient_attribute_write.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_cmd_attclient_attribute_write.vi"/>
				<Item Name="ble_cmd_attclient_find_information.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_cmd_attclient_find_information.vi"/>
				<Item Name="ble_cmd_attclient_read_by_group_type.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_cmd_attclient_read_by_group_type.vi"/>
				<Item Name="ble_cmd_attclient_read_by_handle.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_cmd_attclient_read_by_handle.vi"/>
				<Item Name="ble_cmd_connection_disconnect.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_cmd_connection_disconnect.vi"/>
				<Item Name="ble_cmd_end_procedure.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_cmd_end_procedure.vi"/>
				<Item Name="ble_cmd_gap_connect_direct.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_cmd_gap_connect_direct.vi"/>
				<Item Name="ble_cmd_gap_discover.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_cmd_gap_discover.vi"/>
				<Item Name="ble_cmd_system_get_info.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_cmd_system_get_info.vi"/>
				<Item Name="ble_cmd_system_reset.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_cmd_system_reset.vi"/>
				<Item Name="ble_connect_to_device.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_connect_to_device.vi"/>
				<Item Name="ble_delete_repeated_scan_responses.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_delete_repeated_scan_responses.vi"/>
				<Item Name="ble_disconnect_device.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_disconnect_device.vi"/>
				<Item Name="ble_discover_ble_devices.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_discover_ble_devices.vi"/>
				<Item Name="ble_discover_characteristics.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_discover_characteristics.vi"/>
				<Item Name="ble_discover_handle_table.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_discover_handle_table.vi"/>
				<Item Name="ble_discover_services.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_discover_services.vi"/>
				<Item Name="ble_discover_stop.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_discover_stop.vi"/>
				<Item Name="ble_dongle_info.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_dongle_info.vi"/>
				<Item Name="ble_evt_attclient_attribute_value.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_evt_attclient_attribute_value.vi"/>
				<Item Name="ble_evt_attclient_find_information_found.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_evt_attclient_find_information_found.vi"/>
				<Item Name="ble_evt_attclient_group_found.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_evt_attclient_group_found.vi"/>
				<Item Name="ble_evt_attclient_procedue_completed.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_evt_attclient_procedue_completed.vi"/>
				<Item Name="ble_evt_connection_disconnected.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_evt_connection_disconnected.vi"/>
				<Item Name="ble_evt_connection_status.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_evt_connection_status.vi"/>
				<Item Name="ble_evt_gap_scan_response.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_evt_gap_scan_response.vi"/>
				<Item Name="ble_hex2string.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_hex2string.vi"/>
				<Item Name="ble_parse_scan_response.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_parse_scan_response.vi"/>
				<Item Name="ble_read_by_handle.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_read_by_handle.vi"/>
				<Item Name="ble_read_by_service_characteristic.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_read_by_service_characteristic.vi"/>
				<Item Name="ble_read_message.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_read_message.vi"/>
				<Item Name="ble_read_specific_message.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_read_specific_message.vi"/>
				<Item Name="ble_reset_dongle_and_cnx.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_reset_dongle_and_cnx.vi"/>
				<Item Name="ble_rsp_attclient_attribute_write.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_rsp_attclient_attribute_write.vi"/>
				<Item Name="ble_rsp_attclient_find_information.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_rsp_attclient_find_information.vi"/>
				<Item Name="ble_rsp_attclient_read_by_group_type.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_rsp_attclient_read_by_group_type.vi"/>
				<Item Name="ble_rsp_attclient_read_by_handle.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_rsp_attclient_read_by_handle.vi"/>
				<Item Name="ble_rsp_connection_disconnect.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_rsp_connection_disconnect.vi"/>
				<Item Name="ble_rsp_gap_connect_direct.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_rsp_gap_connect_direct.vi"/>
				<Item Name="ble_rsp_gap_discover.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_rsp_gap_discover.vi"/>
				<Item Name="ble_rsp_gap_end_procedure.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_rsp_gap_end_procedure.vi"/>
				<Item Name="ble_rsp_system_get_info.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/api_commands/ble_rsp_system_get_info.vi"/>
				<Item Name="ble_search_characteristic_declaration_handle.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_search_characteristic_declaration_handle.vi"/>
				<Item Name="ble_search_client_characteristic_conf_handle.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_search_client_characteristic_conf_handle.vi"/>
				<Item Name="ble_search_handle_by_service_and_characteristic.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_search_handle_by_service_and_characteristic.vi"/>
				<Item Name="ble_starts_discovery.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_starts_discovery.vi"/>
				<Item Name="ble_string2hex.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_string2hex.vi"/>
				<Item Name="ble_wait_ms.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_wait_ms.vi"/>
				<Item Name="ble_write_by_handle.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_write_by_handle.vi"/>
				<Item Name="ble_write_by_handle_uint8array.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_write_by_handle_uint8array.vi"/>
				<Item Name="ble_write_by_handle_uint16.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_write_by_handle_uint16.vi"/>
				<Item Name="ble_write_client_characterist_configuration.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/ble_functions/ble_write_client_characterist_configuration.vi"/>
				<Item Name="ble_write_message.vi" Type="VI" URL="/&lt;vilib&gt;/BLE/support_vi/ble_write_message.vi"/>
				<Item Name="BuildHelpPath.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/BuildHelpPath.vi"/>
				<Item Name="Check Special Tags.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Check Special Tags.vi"/>
				<Item Name="Clear Errors.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Clear Errors.vi"/>
				<Item Name="connection_status_bundle.ctl" Type="VI" URL="/&lt;vilib&gt;/BLE/type_def/connection_status_bundle.ctl"/>
				<Item Name="Convert property node font to graphics font.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Convert property node font to graphics font.vi"/>
				<Item Name="Details Display Dialog.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Details Display Dialog.vi"/>
				<Item Name="DialogType.ctl" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/DialogType.ctl"/>
				<Item Name="DialogTypeEnum.ctl" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/DialogTypeEnum.ctl"/>
				<Item Name="Error Code Database.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Error Code Database.vi"/>
				<Item Name="ErrWarn.ctl" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/ErrWarn.ctl"/>
				<Item Name="eventvkey.ctl" Type="VI" URL="/&lt;vilib&gt;/event_ctls.llb/eventvkey.ctl"/>
				<Item Name="Find Tag.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Find Tag.vi"/>
				<Item Name="Format Message String.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Format Message String.vi"/>
				<Item Name="gap_discover_mode.ctl" Type="VI" URL="/&lt;vilib&gt;/BLE/type_def/gap_discover_mode.ctl"/>
				<Item Name="General Error Handler Core CORE.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/General Error Handler Core CORE.vi"/>
				<Item Name="General Error Handler.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/General Error Handler.vi"/>
				<Item Name="Get String Text Bounds.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Get String Text Bounds.vi"/>
				<Item Name="Get Text Rect.vi" Type="VI" URL="/&lt;vilib&gt;/picture/picture.llb/Get Text Rect.vi"/>
				<Item Name="GetHelpDir.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/GetHelpDir.vi"/>
				<Item Name="GetRTHostConnectedProp.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/GetRTHostConnectedProp.vi"/>
				<Item Name="Longest Line Length in Pixels.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Longest Line Length in Pixels.vi"/>
				<Item Name="LVBoundsTypeDef.ctl" Type="VI" URL="/&lt;vilib&gt;/Utility/miscctls.llb/LVBoundsTypeDef.ctl"/>
				<Item Name="LVPoint32TypeDef.ctl" Type="VI" URL="/&lt;vilib&gt;/Utility/miscctls.llb/LVPoint32TypeDef.ctl"/>
				<Item Name="LVPositionTypeDef.ctl" Type="VI" URL="/&lt;vilib&gt;/Utility/miscctls.llb/LVPositionTypeDef.ctl"/>
				<Item Name="LVRectTypeDef.ctl" Type="VI" URL="/&lt;vilib&gt;/Utility/miscctls.llb/LVRectTypeDef.ctl"/>
				<Item Name="message_class.ctl" Type="VI" URL="/&lt;vilib&gt;/BLE/type_def/message_class.ctl"/>
				<Item Name="message_type.ctl" Type="VI" URL="/&lt;vilib&gt;/BLE/type_def/message_type.ctl"/>
				<Item Name="NI_AALBase.lvlib" Type="Library" URL="/&lt;vilib&gt;/Analysis/NI_AALBase.lvlib"/>
				<Item Name="NI_MABase.lvlib" Type="Library" URL="/&lt;vilib&gt;/measure/NI_MABase.lvlib"/>
				<Item Name="NI_MAPro.lvlib" Type="Library" URL="/&lt;vilib&gt;/measure/NI_MAPro.lvlib"/>
				<Item Name="Not Found Dialog.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Not Found Dialog.vi"/>
				<Item Name="Search and Replace Pattern.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Search and Replace Pattern.vi"/>
				<Item Name="service_type.ctl" Type="VI" URL="/&lt;vilib&gt;/BLE/type_def/service_type.ctl"/>
				<Item Name="Set Bold Text.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Set Bold Text.vi"/>
				<Item Name="Set Busy.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/cursorutil.llb/Set Busy.vi"/>
				<Item Name="Set Cursor (Cursor ID).vi" Type="VI" URL="/&lt;vilib&gt;/Utility/cursorutil.llb/Set Cursor (Cursor ID).vi"/>
				<Item Name="Set Cursor (Icon Pict).vi" Type="VI" URL="/&lt;vilib&gt;/Utility/cursorutil.llb/Set Cursor (Icon Pict).vi"/>
				<Item Name="Set Cursor.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/cursorutil.llb/Set Cursor.vi"/>
				<Item Name="Set String Value.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Set String Value.vi"/>
				<Item Name="Simple Error Handler.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Simple Error Handler.vi"/>
				<Item Name="Space Constant.vi" Type="VI" URL="/&lt;vilib&gt;/dlg_ctls.llb/Space Constant.vi"/>
				<Item Name="TagReturnType.ctl" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/TagReturnType.ctl"/>
				<Item Name="Three Button Dialog CORE.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Three Button Dialog CORE.vi"/>
				<Item Name="Three Button Dialog.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Three Button Dialog.vi"/>
				<Item Name="Trim Whitespace.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/Trim Whitespace.vi"/>
				<Item Name="Unset Busy.vi" Type="VI" URL="/&lt;vilib&gt;/Utility/cursorutil.llb/Unset Busy.vi"/>
				<Item Name="VISA Configure Serial Port" Type="VI" URL="/&lt;vilib&gt;/Instr/_visa.llb/VISA Configure Serial Port"/>
				<Item Name="VISA Configure Serial Port (Instr).vi" Type="VI" URL="/&lt;vilib&gt;/Instr/_visa.llb/VISA Configure Serial Port (Instr).vi"/>
				<Item Name="VISA Configure Serial Port (Serial Instr).vi" Type="VI" URL="/&lt;vilib&gt;/Instr/_visa.llb/VISA Configure Serial Port (Serial Instr).vi"/>
				<Item Name="whitespace.ctl" Type="VI" URL="/&lt;vilib&gt;/Utility/error.llb/whitespace.ctl"/>
			</Item>
			<Item Name="Determine Clicked Array Element Index - Calculate Gapped and Un-Gapped Coordinates.vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/_Subs/Determine Clicked Array Element Index - Calculate Gapped and Un-Gapped Coordinates.vi"/>
			<Item Name="Determine Clicked Array Element Index - Calculate Scrollbar Edges.vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/_Subs/Determine Clicked Array Element Index - Calculate Scrollbar Edges.vi"/>
			<Item Name="Determine Clicked Array Element Index - Cancel Error Code 1320.vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/_Subs/Determine Clicked Array Element Index - Cancel Error Code 1320.vi"/>
			<Item Name="Determine Clicked Array Element Index - Determine Array and Scrollbar Properties.vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/_Subs/Determine Clicked Array Element Index - Determine Array and Scrollbar Properties.vi"/>
			<Item Name="Determine Clicked Array Element Index - Determine Array Element Coordinates.vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/_Subs/Determine Clicked Array Element Index - Determine Array Element Coordinates.vi"/>
			<Item Name="Determine Clicked Array Element Index - Determine if Item Clicked.vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/_Subs/Determine Clicked Array Element Index - Determine if Item Clicked.vi"/>
			<Item Name="Determine Clicked Array Element Index - Determine Index Values (1D or 2D Array).vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/_Subs/Determine Clicked Array Element Index - Determine Index Values (1D or 2D Array).vi"/>
			<Item Name="Determine Clicked Array Element Index - Get Minimal Array Properties.vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/_Subs/Determine Clicked Array Element Index - Get Minimal Array Properties.vi"/>
			<Item Name="Determine Clicked Array Element Index - Test For Array Element Gap.vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/_Subs/Determine Clicked Array Element Index - Test For Array Element Gap.vi"/>
			<Item Name="Determine Clicked Array Element Index - Test For Error Code 1320.vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/_Subs/Determine Clicked Array Element Index - Test For Error Code 1320.vi"/>
			<Item Name="Determine Clicked Array Element Index.vi" Type="VI" URL="../subs/Determine Clicked Array Element Index 2011/Determine Clicked Array Element Index.vi"/>
			<Item Name="lvanlys.dll" Type="Document" URL="/&lt;resource&gt;/lvanlys.dll"/>
			<Item Name="NordicDatabaseDLL.dll" Type="Document" URL="../DLLs/NordicDatabaseDLL.dll"/>
		</Item>
		<Item Name="Build Specifications" Type="Build">
			<Item Name="Patient Monitoring Device" Type="EXE">
				<Property Name="App_copyErrors" Type="Bool">true</Property>
				<Property Name="App_INI_aliasGUID" Type="Str">{F23B278C-4840-4841-A68A-F69950EB997F}</Property>
				<Property Name="App_INI_GUID" Type="Str">{B5CA37B7-CEC0-4895-8B17-331F885B8341}</Property>
				<Property Name="App_serverConfig.httpPort" Type="Int">8002</Property>
				<Property Name="Bld_autoIncrement" Type="Bool">true</Property>
				<Property Name="Bld_buildCacheID" Type="Str">{59B08B46-4C79-4C0A-9E76-AE7A65161AFB}</Property>
				<Property Name="Bld_buildSpecName" Type="Str">Patient Monitoring Device</Property>
				<Property Name="Bld_excludeInlineSubVIs" Type="Bool">true</Property>
				<Property Name="Bld_excludeLibraryItems" Type="Bool">true</Property>
				<Property Name="Bld_excludePolymorphicVIs" Type="Bool">true</Property>
				<Property Name="Bld_localDestDir" Type="Path">../builds/Ptient Monitroing Device</Property>
				<Property Name="Bld_localDestDirType" Type="Str">relativeToProject</Property>
				<Property Name="Bld_modifyLibraryFile" Type="Bool">true</Property>
				<Property Name="Bld_previewCacheID" Type="Str">{94631617-0ED2-47E6-AF90-28F1C2FF81C8}</Property>
				<Property Name="Bld_version.build" Type="Int">3</Property>
				<Property Name="Bld_version.major" Type="Int">1</Property>
				<Property Name="Destination[0].destName" Type="Str">Patinet Monitoring Device.exe</Property>
				<Property Name="Destination[0].path" Type="Path">../builds/Ptient Monitroing Device/Patinet Monitoring Device.exe</Property>
				<Property Name="Destination[0].path.type" Type="Str">relativeToProject</Property>
				<Property Name="Destination[0].preserveHierarchy" Type="Bool">true</Property>
				<Property Name="Destination[0].type" Type="Str">App</Property>
				<Property Name="Destination[1].destName" Type="Str">Support Directory</Property>
				<Property Name="Destination[1].path" Type="Path">../builds/Ptient Monitroing Device/data</Property>
				<Property Name="Destination[1].path.type" Type="Str">relativeToProject</Property>
				<Property Name="DestinationCount" Type="Int">2</Property>
				<Property Name="Exe_iconItemID" Type="Ref">/My Computer/ctrls/Heart Monitor-50.ico</Property>
				<Property Name="Source[0].itemID" Type="Str">{77451004-2573-4A88-9F37-0CC2BEEBDEF9}</Property>
				<Property Name="Source[0].type" Type="Str">Container</Property>
				<Property Name="Source[1].destinationIndex" Type="Int">0</Property>
				<Property Name="Source[1].itemID" Type="Ref">/My Computer/main.vi</Property>
				<Property Name="Source[1].sourceInclusion" Type="Str">TopLevel</Property>
				<Property Name="Source[1].type" Type="Str">VI</Property>
				<Property Name="SourceCount" Type="Int">2</Property>
				<Property Name="TgtF_fileDescription" Type="Str">Patient Monitoring Device</Property>
				<Property Name="TgtF_internalName" Type="Str">Patient Monitoring Device</Property>
				<Property Name="TgtF_legalCopyright" Type="Str">Copyright © 2017 </Property>
				<Property Name="TgtF_productName" Type="Str">Patient Monitoring Device</Property>
				<Property Name="TgtF_targetfileGUID" Type="Str">{43EF3439-D0E2-48C9-A3A5-4B1CAAC65ADD}</Property>
				<Property Name="TgtF_targetfileName" Type="Str">Patinet Monitoring Device.exe</Property>
			</Item>
		</Item>
	</Item>
</Project>
