﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ModManager_Classes.src.Enums
{
    public enum DlcId
    {
        [EnumMember] Anarchist,
        [EnumMember] SunkenTreasure,
        [EnumMember] Botanica,
        [EnumMember] ThePassage,
        [EnumMember] SeatOfPower,
        [EnumMember] BrightHarvest,
        [EnumMember] LandOfLions,
        [EnumMember] Christmas,
        [EnumMember] AmusementPark,
        [EnumMember] CityLife,
        [EnumMember] Docklands,
        [EnumMember] Tourism,
        [EnumMember] Highlife,
        [EnumMember] VehicleSkins,
        [EnumMember] VibrantCity,
        [EnumMember] PedestrianZone
    }

    public enum DlcRequirement
    {
        [EnumMember] required,
        [EnumMember] partly,
        [EnumMember] atLeastOneRequired
    }

    public enum ApplicationLanguage
    { 
        [EnumMember] English, 
        [EnumMember] German
    }
}
