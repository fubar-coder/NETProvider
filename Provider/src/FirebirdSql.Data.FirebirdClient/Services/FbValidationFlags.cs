﻿/*
 *    The contents of this file are subject to the Initial
 *    Developer's Public License Version 1.0 (the "License");
 *    you may not use this file except in compliance with the
 *    License. You may obtain a copy of the License at
 *    https://github.com/FirebirdSQL/NETProvider/raw/master/license.txt.
 *
 *    Software distributed under the License is distributed on
 *    an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either
 *    express or implied. See the License for the specific
 *    language governing rights and limitations under the License.
 *
 *    All Rights Reserved.
 */

//$Authors = Carlos Guzman Alvarez

using System;
using FirebirdSql.Data.Common;

namespace FirebirdSql.Data.Services
{
	[Flags]
	public enum FbValidationFlags
	{
		ValidateDatabase = IscCodes.isc_spb_rpr_validate_db,
		SweepDatabase = IscCodes.isc_spb_rpr_sweep_db,
		MendDatabase = IscCodes.isc_spb_rpr_mend_db,
		CheckDatabase = IscCodes.isc_spb_rpr_check_db,
		IgnoreChecksum = IscCodes.isc_spb_rpr_ignore_checksum,
		KillShadows = IscCodes.isc_spb_rpr_kill_shadows,
		Full = IscCodes.isc_spb_rpr_full,
	}
}
