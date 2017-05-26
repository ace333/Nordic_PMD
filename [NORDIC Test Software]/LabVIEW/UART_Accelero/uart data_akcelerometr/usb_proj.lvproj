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
		<Item Name="DB" Type="Folder">
			<Item Name="dbTest.vi" Type="VI" URL="../dbTest.vi"/>
			<Item Name="dbInsert_subVI.vi" Type="VI" URL="../dbInsert_subVI.vi"/>
		</Item>
		<Item Name="Controls" Type="Folder">
			<Item Name="data_typedef.ctl" Type="VI" URL="../data_typedef.ctl"/>
			<Item Name="enum_typedef.ctl" Type="VI" URL="../enum_typedef.ctl"/>
			<Item Name="main_data_typedef.ctl" Type="VI" URL="../main_data_typedef.ctl"/>
			<Item Name="DBEnum.ctl" Type="VI" URL="../DBEnum.ctl"/>
		</Item>
		<Item Name="SubVI" Type="Folder">
			<Item Name="check_data (SubVI).vi" Type="VI" URL="../check_data (SubVI).vi"/>
			<Item Name="check_tables (SubVI).vi" Type="VI" URL="../check_tables (SubVI).vi"/>
			<Item Name="deleteEndLine (SubVI).vi" Type="VI" URL="../deleteEndLine (SubVI).vi"/>
			<Item Name="edit_data (SubVI).vi" Type="VI" URL="../edit_data (SubVI).vi"/>
			<Item Name="manage_accelero (SubVI).vi" Type="VI" URL="../manage_accelero (SubVI).vi"/>
			<Item Name="manage_errors (SubVI).vi" Type="VI" URL="../manage_errors (SubVI).vi"/>
			<Item Name="show_measure (SubVI).vi" Type="VI" URL="../show_measure (SubVI).vi"/>
			<Item Name="FGV.vi" Type="VI" URL="../FGV.vi"/>
		</Item>
		<Item Name="Main" Type="Folder">
			<Item Name="uart data_main.vi" Type="VI" URL="../uart data_main.vi"/>
			<Item Name="uart data_main — kopia.vi" Type="VI" URL="../uart data_main — kopia.vi"/>
		</Item>
		<Item Name="testVI.vi" Type="VI" URL="../testVI.vi"/>
		<Item Name="Dependencies" Type="Dependencies">
			<Item Name="vi.lib" Type="Folder">
				<Item Name="VISA Configure Serial Port" Type="VI" URL="/&lt;vilib&gt;/Instr/_visa.llb/VISA Configure Serial Port"/>
				<Item Name="VISA Configure Serial Port (Instr).vi" Type="VI" URL="/&lt;vilib&gt;/Instr/_visa.llb/VISA Configure Serial Port (Instr).vi"/>
				<Item Name="VISA Configure Serial Port (Serial Instr).vi" Type="VI" URL="/&lt;vilib&gt;/Instr/_visa.llb/VISA Configure Serial Port (Serial Instr).vi"/>
			</Item>
			<Item Name="NordicDatabaseDLL.dll" Type="Document" URL="../../../../../Visual Studio 2015/Projects/NordicDatabaseDLL/NordicDatabaseDLL/bin/Debug/NordicDatabaseDLL.dll"/>
		</Item>
		<Item Name="Build Specifications" Type="Build"/>
	</Item>
</Project>
