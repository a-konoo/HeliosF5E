﻿//  Copyright 2014 Craig Courtney
//    
//  Helios is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Helios is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

#pragma warning disable IDE0051 // Remove unused private member: interfaces contain definitions we don't implement
namespace GadrocsWorkshop.Helios.Interfaces.DCS.M2000C
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using GadrocsWorkshop.Helios.Interfaces.DCS.Common;


    [HeliosInterface("Helios.M2000C", "DCS M-2000C", typeof(DCSInterfaceEditor), typeof(UniqueHeliosInterfaceFactory), UniquenessKey = "Helios.DCSInterface")]
    public class M2000CInterface : DCSInterface
    {
        #region Devices
        private const string FLIGHTINST = "1";
        private const string NAVINST = "2";
        private const string ENGINE = "3";
        private const string INSTPANEL = "4";
        private const string VTH_VTB = "5";
        private const string VTB = "5";
        private const string PCA_PPA = "6";
        private const string PCA = "6";
        private const string ENGPANEL = "7";
        private const string PWRPNL = "8";
        private const string PCN_NAV = "9";
        private const string PCN = "9";
        private const string RADAR_RDI = "10";
        private const string INS = "10";
        private const string RADAR = "11";
        private const string PCR = "11";
        private const string EW_RWR = "12";
        private const string SERVAL = "12";	        // 12	SERVAL & ECLAIR (Counter Measure dispensers)
        private const string RWR = "13";
        private const string PCCM = "13";		    // 13	Poste de Controle Contre Measures (Countermeasures panel)
        private const string SUBSYSTEMS = "14";
        private const string HYDRAULICS = "14"; 
        private const string MAGIC = "15";
        private const string SYSLIGHTS = "16";
        private const string AFCS = "17";
        private const string CDVE = "17";
        private const string ELECTRIC = "18";
        private const string UVHF = "19";
        private const string VHF = "19";
        private const string UHF = "20";
        private const string INTERCOM = "21";
        private const string MISCPANELS = "22";
        private const string TACAN = "23";
        private const string VORILS = "24";
        private const string ECS = "25";
        private const string FBW = "26";
        private const string DDM = "27";	            // 27	Detecteur de Depart Missile (MLWS)
        private const string DDM_IND = "28";
        private const string SPIRALE = "28";            // 28	SPIRALE (Countermeasure dispenser)
        private const string WEAPONS_CONTROL = "29";
        private const string CAS = "29";                // 29	Calculateur Air Sol (Air Ground Calculator)
        private const string SNA = "30";				// 30	Systeme de navigation et d'attaque
        private const string Helmet_NVG = "31";         // 31
        private const string PPA = "32";                // 32	Panneau de Préparation Armement (Weapon Preparation Panel)
        private const string RDI = "33";                // 33	Radar Doppler Impulsions (main radar)
        private const string DATABUS = "34";            // 34	
        private const string CCLT = "35";               // 35	Calcul Continu Ligne Tracantes (Gun snake)
        private const string VTH = "36";                // 36	Visee Tete Haute (Head Up Display)
        private const string CAA = "37";                // 37	Calculateur Air Air	(Air Air Calculator)
        private const string RS = "38";                 // 38	Radio Sonde	(Radar Altimeter)
        private const string TAF = "39";                // 39	Teleaffichage (Datalink)
        private const string CANOPY = "40";             // 40
        private const string SABRE = "41";              // 41	SABRE (Jammer)

        private const string KNEEBOARD = "100";

        #endregion
        #region Ids
        private const string CMD = "3";
        private const string UVHF_PRESET_DISPLAY = "436";
        private const string UVHF_PRESET_KNOB = "445";
        private const string VORILS_DISPLAY = "611";
        private const string VORILS_FREQUENCY_CHANGE_WHOLE = "616";
        private const string VORILS_FREQUENCY_CHANGE_DECIMAL = "618";
        private const string VORILS_POWER = "617";
        private const string TACAN_DISPLAY = "621";
        private const string TACAN_C10_SELECTOR = "623";
        private const string TACAN_XY_SELECTOR = "624";
        private const string TACAN_C1_SELECTOR = "625";
        private const string TACAN_MODE_SELECTOR = "626";
        #endregion
        #region Commands Codes from command_defs.lua (note:  codes do not necessarily match the enumeration name) 
        private enum initial_Commands
        {
            Button_1 = 3001,
            Button_2 = 3002,
            Button_3 = 3003,
            Button_4 = 3004,
            Button_5 = 3005,
            Button_6 = 3006,
            Button_7 = 3007,
            Button_8 = 3008,
            Button_9 = 3009,
            Button_10 = 3010,
            Button_11 = 3011,
            Button_12 = 3012,
            Button_13 = 3013,
            Button_14 = 3014,
            Button_15 = 3015,
            Button_16 = 3016,
            Button_17 = 3017,
            Button_18 = 3018,
            Button_19 = 3019,
            Button_20 = 3020,
            Button_21 = 3021,
            Button_22 = 3022,
            Button_23 = 3023,
            Button_24 = 3024,
            Button_25 = 3025,
            Button_26 = 3026,
            Button_27 = 3027,
            Button_28 = 3028,
            Button_29 = 3029,
            Button_30 = 3030,
            Button_31 = 3031,
            Button_32 = 3032,
            Button_33 = 3033,
            Button_34 = 3034,
            Button_35 = 3035,
            Button_36 = 3036,
            Button_37 = 3037,
            Button_38 = 3038,
            Button_39 = 3039,
            Button_40 = 3040,
            Button_41 = 3041,
            Button_42 = 3042,
            Button_43 = 3043,
            Button_44 = 3044,
            Button_45 = 3045,
            Button_46 = 3046,
            Button_47 = 3047,
            Button_48 = 3048,
            Button_49 = 3049,
            Button_50 = 3050,
            Button_51 = 3051,
            Button_52 = 3052,
            Button_53 = 3053,
            Button_54 = 3054,
            Button_55 = 3055,
            Button_56 = 3056,
            Button_57 = 3057,
            Button_58 = 3058,
            Button_59 = 3059,
            Button_60 = 3060,
            Button_61 = 3061,
            Button_62 = 3062,
            Button_63 = 3063,
            Button_64 = 3064,
            Button_65 = 3065,
            Button_66 = 3066,
            Button_67 = 3067,
            Button_68 = 3068,
            Button_69 = 3069,
            Button_70 = 3070,
            Button_81 = 3081,
            Button_82 = 3082,
            Button_83 = 3083,
            Button_84 = 3084,
            Button_85 = 3085,
            Button_86 = 3086,
            Button_87 = 3087,
            Button_88 = 3088,
            Button_89 = 3089,
            Button_90 = 3090,
            Button_91 = 3091,
            Button_92 = 3092,
            Button_93 = 3093,
            Button_94 = 3094,
            Button_95 = 3095,
            Button_96 = 3096,
            Button_97 = 3097,
            Button_98 = 3098,
            Button_99 = 3099,
            Button_100 = 3100,
            Button_101 = 3101,
            Button_102 = 3102,
            Button_103 = 3103,
            Button_104 = 3104,
            Button_105 = 3105,
            Button_106 = 3106,
            Button_107 = 3107,
            Button_108 = 3108,
            Button_109 = 3109,
            Button_110 = 3110,
            Button_111 = 3111,
            Button_112 = 3112,
            Button_113 = 3113,
            Button_114 = 3114,
            Button_115 = 3115,
            Button_116 = 3116,
            Button_117 = 3117,
            Button_118 = 3118,
            Button_119 = 3119,
            Button_120 = 3120,
            Button_121 = 3121,
            Button_122 = 3122,
            Button_123 = 3123,
            Button_124 = 3124,
            Button_125 = 3125,
            Button_126 = 3126,
            Button_127 = 3127,
            Button_128 = 3128,
            Button_129 = 3129,
            Button_130 = 3130,
            Button_131 = 3131,
            Button_132 = 3132,
            Button_133 = 3133,
            Button_134 = 3134,
            Button_135 = 3135,
            Button_136 = 3136,
            Button_137 = 3137,
            Button_138 = 3138,
            Button_139 = 3139,
            Button_140 = 3140,
            Button_141 = 3141,
            Button_142 = 3142,
            Button_143 = 3143,
            Button_144 = 3144,
            Button_145 = 3145,
            Button_146 = 3146,
            Button_147 = 3147,
            Button_148 = 3148,
            Button_149 = 3149,
            Button_150 = 3150,
            Button_151 = 3151,
            Button_152 = 3152,
            Button_153 = 3153,
            Button_154 = 3154,
            Button_155 = 3155,
            Button_156 = 3156,
            Button_157 = 3157,
            Button_158 = 3158,
            Button_159 = 3159,
            Button_160 = 3160,
            Button_161 = 3161,
            Button_162 = 3162,
            Button_163 = 3163,
            Button_164 = 3164,
            Button_165 = 3165,
            Button_166 = 3166,
            Button_167 = 3167,
            Button_168 = 3168,
            Button_169 = 3169,
            Button_170 = 3170,
            Button_171 = 3171,
            Button_172 = 3172,
            Button_173 = 3173,
            Button_174 = 3174,
            Button_175 = 3175,
            Button_176 = 3176,
            Button_177 = 3177,
            Button_178 = 3178,
            Button_179 = 3179,
            Button_180 = 3180,
            Button_181 = 3181,
            Button_182 = 3182,
            Button_183 = 3183,
            Button_184 = 3184,
            Button_185 = 3185,
            Button_186 = 3186,
            Button_187 = 3187,
            Button_188 = 3188,
            Button_189 = 3189,
            Button_190 = 3190,
            Button_191 = 3191
        };
        private enum Unassigned_Commands
        {
            Button_198 = 3198,
            Button_199 = 3199,
            Button_200 = 3200
        };
        private enum ECM_Box_Commands
        {
            Button_194 = 3194,
            Button_195 = 3195,
            Button_196 = 3196,
            Button_197 = 3197
        };

        private enum VTH_VTB_Commands
        {
            Button_192 = 3192,
            Button_201 = 3201,
            Button_202 = 3202,
            Button_203 = 3203,
            Button_204 = 3204,
            Button_205 = 3205,
            Button_206 = 3206,
            Button_207 = 3207,
            Button_208 = 3208,
            Button_209 = 3209,
            Button_210 = 3210,
            Button_244 = 3244,
            Button_213 = 3213,
            Button_214 = 3214,
            Button_215 = 3215,
            Button_216 = 3216,
            Button_217 = 3217,
            Button_218 = 3218,
            Button_219 = 3219,
            Button_220 = 3220,
            Button_221 = 3221,
            Button_222 = 3222,
            Button_223 = 3223,
            Button_224 = 3224,
            Button_225 = 3225,
            Button_226 = 3226,
            Button_227 = 3227,
            Button_470 = 3470
        };

        private enum RWR_Commands
        {
            Button_228 = 3228
        };

        private enum PCA_PPA_Commands
        {
            Button_234 = 3234,
            Button_235 = 3235,
            Button_237 = 3237,
            Button_239 = 3239,
            Button_241 = 3241,
            Button_243 = 3243,
            Button_245 = 3245,
            Button_247 = 3247,
            Button_248 = 3248,
            Button_249 = 3249,
            Button_250 = 3250,
            Button_253 = 3253,
            Button_256 = 3256,
            Button_259 = 3259,
            Button_262 = 3262,
            Button_265 = 3265,
            Button_266 = 3266,
            Button_269 = 3269,
            Button_272 = 3272,
            Button_275 = 3275,
            Button_276 = 3276,
            Button_277 = 3277,
            Button_278 = 3278,
            Button_279 = 3279,
            Button_409 = 3409
        };

        private enum CDVE_Commands
        {
            Button_282 = 3282,
            Button_285 = 3285,
            Button_288 = 3288,
            Button_291 = 3291,
            Button_294 = 3294,
            Button_299 = 3299,
            Button_300 = 3300,
            Button_301 = 3301,
            Button_302 = 3302,
            Button_514 = 3514,
            Button_515 = 3515,
            Button_516 = 3516,
            Button_517 = 3517
        };

        private enum Cpt_Clock_Commands
        {
            Button_400 = 3400
        };

        private enum FBW_Commands
        {
            Button_330 = 3330,
            Button_420 = 3420,
            Button_421 = 3421,
            Button_422 = 3422,
            Button_423 = 3423,
            Button_479 = 3479,
            Button_480 = 3480
        };

        private enum ALT_ADI_Commands
        {
            Button_309 = 3309,
            Button_314 = 3314,
            Button_315 = 3315,
            Button_325 = 3325,
            Button_328 = 3328
        };

        private enum HSI_Commands
        {
            Button_340 = 3340,
            Button_341 = 3341
        };

        private enum GMeter_Commands
        {
            Button_348 = 3348
        };

        private enum IFF_Mode_Panel_Commands
        {
            Button_377 = 3377,
            Button_378 = 3378,
            Button_379 = 3379,
            Button_380 = 3380,
            Button_381 = 3381,
            Button_382 = 3382,
            Button_383 = 3383,
            Button_384 = 3384,
            Button_385 = 3385,
            Button_386 = 3386,
            Button_387 = 3387
        };

        private enum COMM_PANEL_Commands
        {
            Button_429 = 3429,
            Button_430 = 3430,
            Button_431 = 3431,
            Button_432 = 3432,
            Button_433 = 3433,
            Button_434 = 3434,
            Button_435 = 3435,
            Button_437 = 3437,
            Button_438 = 3438,
            Button_439 = 3439,
            Button_440 = 3440,
            Button_441 = 3441,
            Button_442 = 3442,
            Button_443 = 3443,
            Button_444 = 3444,
            Button_445 = 3445,
            Button_446 = 3446,
            Button_447 = 3447,
            Button_448 = 3448
        };

        private enum VOR_ILS_TACAN_Commands
        {
            Button_616 = 3616,
            Button_617 = 3617,
            Button_618 = 3618,
            Button_619 = 3619,
            Button_623 = 3623,
            Button_624 = 3624,
            Button_625 = 3625,
            Button_626 = 3626
        };

        private enum EXT_LIGHTS_CONTROLS_Commands
        {
            Button_449 = 3449,
            Button_450 = 3450,
            Button_452 = 3452,
            Button_453 = 3453,
            Button_454 = 3454,
            Button_455 = 3455
        };

        private enum SKID_SWITCH_Commands
        {
            Button_458 = 3458,
            Button_459 = 3459
        };

        private enum FLIGHT_CONTROLS_Commands
        {
            Button_396 = 3396,
            Button_404 = 3404,  // Landing Gear lever
            Button_408 = 3408,  // Landing Gear emergency release lever
            Button_926 = 3926,  // Landing Gear Beep
            Button_460 = 3460,
            Button_461 = 3461,
            Button_462 = 3462
        };


        private enum THROTTLE_Commands
        {
            Button_467 = 3467,
            Button_468 = 3468
        };

        private enum RADAR_Commands
        {
            Button_109 = 3109,
            Button_481 = 3481,
            Button_482 = 3482,
            Button_483 = 3483,
            Button_484 = 3484,
            Button_485 = 3485,
            Button_486 = 3486,
            Button_488 = 3488,
            Button_489 = 3489,
            Button_491 = 3491,
            Button_493 = 3493,
            Button_495 = 3495,
            Button_497 = 3497,
            Button_499 = 3499,
            Button_500 = 3500,
            Button_501 = 3501,
            Button_502 = 3502,
            Button_503 = 3503,
            Button_504 = 3504,
            Button_506 = 3506,
            Button_709 = 3709,
            Button_710 = 3710,
            Button_508 = 3508,
            Button_509 = 3509
        };

        private enum ELECTRICAL_Commands
        {
            Button_520 = 3520,  // Main battery switch
            Button_521 = 3521,  // Power transfer switch
            Button_522 = 3522,  // Alternator 1 switch
            Button_523 = 3523,  // Alternator 2 switch
            Button_524 = 3524,  // Warning lights test switch
            Button_654 = 3654   // QRA switch
        };

        private enum PCN_NAV_Commands
        {
            Button_570 = 3570,  // PREP
            Button_572 = 3572,  // DEST
            Button_574 = 3574,  // Mode Knob
            Button_575 = 3575,  // Brightness Knob
            Button_576 = 3576,  // BAD Button
            Button_578 = 3578,  // REC Button
            Button_580 = 3580,  // VAL Button
            Button_582 = 3582,  // MRQ Button
            Button_667 = 3667,  // ENC Button
            Button_584 = 3584,  // Keyb 1
            Button_585 = 3585,
            Button_586 = 3586,
            Button_587 = 3587,
            Button_588 = 3588,
            Button_589 = 3589,
            Button_590 = 3590,
            Button_591 = 3591,
            Button_592 = 3592,
            Button_593 = 3593,  // Keyb 0
            Button_594 = 3594,  // Clear Button
            Button_596 = 3596,  // Insert Button
            Button_627 = 3627,  // INS Op Mode
            Button_629 = 3629,  // INS Sts Mode
            Button_665 = 3665,  // Aux gyro
            Button_628 = 3628,  // MIP slot door
            Button_673 = 3673,  // MIP label click
            Button_674 = 3674,  // MIP label wheel
            Button_675 = 3675   // MIP top click
        };

        private enum RADAR_IFF_Commands
        {
            Button_598 = 3598,
            Button_599 = 3599,
            Button_600 = 3600,
            Button_601 = 3601,
            Button_602 = 3602,
            Button_603 = 3603,
            Button_604 = 3604
        };

        private enum EW_Commands
        {
            Button_605 = 3605,
            Button_606 = 3606,
            Button_607 = 3607,
            Button_608 = 3608,
            Button_609 = 3609,
            Button_610 = 3610,
            Button_990 = 3990
        };

        private enum ECS_Commands
        {
            Button_630 = 3630,
            Button_631 = 3631,
            Button_633 = 3633,
            Button_635 = 3635,
            Button_636 = 3636,
            Button_637 = 3637,
            Button_638 = 3638
        };

        private enum PANEL_LIGHTS_Commands
        {
            Button_639 = 3639,
            Button_640 = 3640,
            Button_641 = 3641,
            Button_642 = 3642,
            Button_643 = 3643,
            Button_644 = 3644,
            Button_672 = 3672
        };

        private enum ENGINE_START_PANEL
        {
            Button_645 = 3645,
            Button_646 = 3646,
            Button_647 = 3647,
            Button_648 = 3648,
            Button_649 = 3649,
            Button_650 = 3650,
            Button_651 = 3651,
            Button_652 = 3652,
            Button_477 = 3477,
            Button_478 = 3478,
            Button_471 = 3471,
            Button_472 = 3472,
            Button_463 = 3463,
            Button_464 = 3464,
            Button_465 = 3465,
            Button_473 = 3473,
            Button_474 = 3474,
            Button_475 = 3475,
            Button_476 = 3476,
            Button_193 = 3193
        };

        private enum FUEL_PANEL_Commands
        {
            Button_355 = 3355,
            Button_357 = 3357,
            Button_360 = 3360,
            Button_361 = 3361
        };

        private enum MISC_Commands
        {
            Button_395 = 3395,  // Hydraulic System Selector for gauge reading
            Button_655 = 3655,  // Canopy Open/Close
            Button_656 = 3656,  // Canopy Open/Close
            Button_907 = 3907,  // Canopy Open/Close
            Button_908 = 3908,  // Canopy Open/Close
            Button_657 = 3657,  // Hydraulic Emergency Pump
            Button_658 = 3658,  // Audio warning reset
            Button_659 = 3659,
            Button_660 = 3660,
            Button_666 = 3666,  // Parking brake handle
            Button_900 = 3900,
            Button_807 = 3807,
            Button_457 = 3457,
            Button_456 = 3456
        };

        private enum Sound_panel_Commands
        {
            Button_700 = 3700,
            Button_701 = 3701,
            Button_702 = 3702,
            Button_703 = 3703,
            Button_704 = 3704,
            Button_705 = 3705,
            Button_706 = 3706,
            Button_707 = 3707,
            Button_905 = 3905,
            Button_906 = 3906,
            Button_909 = 3909,
            Button_915 = 3915
        };

        private enum LOX_Commands
        {
            Button_910 = 3910,
            Button_911 = 3911,
            Button_912 = 3912
        };

        private enum VHF_Commands
        {
            Button_950 = 3950,
            Button_951 = 3951,
            Button_952 = 3952,
            Button_953 = 3953,
            Button_954 = 3954,
            Button_955 = 3955,
            Button_956 = 3956,
            Button_957 = 3957,
            Button_958 = 3958,
            Button_959 = 3959,
            Button_960 = 3960,
            Button_961 = 3961,
            Button_962 = 3962,
            Button_963 = 3963,
            Button_964 = 3964,
            Button_965 = 3965,
            Button_966 = 3966,
            Button_967 = 3967,
            Button_1004 = 3968,
            Button_1005 = 3969,
            Button_1006 = 3970
        };

        private enum EVF_Commands
        {
            Button_1004 = 3968,
            Button_1005 = 3969,
            Button_1006 = 3970
        };

        #endregion

        public M2000CInterface()
            : base("DCS M2000C", "M-2000C", "pack://application:,,,/Helios;component/Interfaces/DCS/M2000C/ExportFunctions.lua")
        {

            // see if we can restore from JSON
#if (!DEBUG)
                        if (LoadFunctionsFromJson())
                        {
                            return;
                        }
#endif


            #region Caution Panel
            AddFunction(new FlagValue(this, "525", "Caution Panel", "BATT", "WP BATT"));
            AddFunction(new FlagValue(this, "526", "Caution Panel", "TR", "TR"));
            AddFunction(new FlagValue(this, "527", "Caution Panel", "ALT1", "ALT 1"));
            AddFunction(new FlagValue(this, "528", "Caution Panel", "ALT2", "ALT 2"));
            AddFunction(new FlagValue(this, "529", "Caution Panel", "HUILE", "HUILLE"));
            AddFunction(new FlagValue(this, "530", "Caution Panel", "T7", "T7"));
            AddFunction(new FlagValue(this, "531", "Caution Panel", "CALC", "CALC"));
            AddFunction(new FlagValue(this, "532", "Caution Panel", "SOURIS", "SOURIS"));
            AddFunction(new FlagValue(this, "533", "Caution Panel", "PELLE", "PELLE"));
            AddFunction(new FlagValue(this, "534", "Caution Panel", "BP", "B.P"));
            AddFunction(new FlagValue(this, "535", "Caution Panel", "BPG", "BP.G"));
            AddFunction(new FlagValue(this, "536", "Caution Panel", "BPD", "BP.D"));
            AddFunction(new FlagValue(this, "537", "Caution Panel", "TRANSF", "TRANSF"));
            AddFunction(new FlagValue(this, "538", "Caution Panel", "NIVEAU", "NIVEAU"));
            AddFunction(new FlagValue(this, "539", "Caution Panel", "HYD1", "HYD 1"));
            AddFunction(new FlagValue(this, "540", "Caution Panel", "HYD2", "HYD 2"));
            AddFunction(new FlagValue(this, "541", "Caution Panel", "HYDS", "HYD S"));
            AddFunction(new FlagValue(this, "542", "Caution Panel", "EP", "EP"));
            AddFunction(new FlagValue(this, "543", "Caution Panel", "BINGO", "BINGO"));
            AddFunction(new FlagValue(this, "544", "Caution Panel", "PCAB", "P.CAB"));
            AddFunction(new FlagValue(this, "545", "Caution Panel", "TEMP", "TEMP"));
            AddFunction(new FlagValue(this, "546", "Caution Panel", "REGO2", "REG O2"));
            AddFunction(new FlagValue(this, "547", "Caution Panel", "5mnO2", "5mn O2"));
            AddFunction(new FlagValue(this, "548", "Caution Panel", "O2HA", "O2 HA"));
            AddFunction(new FlagValue(this, "549", "Caution Panel", "ANEMO", "ANEMO"));
            AddFunction(new FlagValue(this, "550", "Caution Panel", "CC", "CC"));
            AddFunction(new FlagValue(this, "551", "Caution Panel", "DSV", "DSV"));
            AddFunction(new FlagValue(this, "552", "Caution Panel", "CONDIT", "CONDIT"));
            AddFunction(new FlagValue(this, "553", "Caution Panel", "CONF", "CONF"));
            AddFunction(new FlagValue(this, "554", "Caution Panel", "PA", "PA"));
            AddFunction(new FlagValue(this, "555", "Caution Panel", "MAN", "MAN"));
            AddFunction(new FlagValue(this, "556", "Caution Panel", "DOM", "DOM"));
            AddFunction(new FlagValue(this, "557", "Caution Panel", "BECS", "BECS"));
            AddFunction(new FlagValue(this, "558", "Caution Panel", "USEL", "USEL"));
            AddFunction(new FlagValue(this, "559", "Caution Panel", "ZEICHEN", "ZEICHEN"));
            AddFunction(new FlagValue(this, "560", "Caution Panel", "GAIN", "GAIN"));
            AddFunction(new FlagValue(this, "561", "Caution Panel", "RPM", "RPM"));
            AddFunction(new FlagValue(this, "562", "Caution Panel", "DECOL", "DECOL"));
            AddFunction(new FlagValue(this, "563", "Caution Panel", "PARK", "PARK"));
            AddFunction(new Switch(this, PWRPNL, "520", new SwitchPosition[] {
                new SwitchPosition("1.0", "OFF", "3520"),
                new SwitchPosition("0.0", "ON", "3520")},
                "Caution Panel", "Main Battery Switch", "%0.1f"));
            AddFunction(new Switch(this, PWRPNL, "521", new SwitchPosition[] {
                new SwitchPosition("1.0", "OFF", "3521"),
                new SwitchPosition("0.0", "ON", "3521")},
                "Caution Panel", "Electric Power Transfer Switch", "%0.1f"));
            AddFunction(new Switch(this, PWRPNL, "522", new SwitchPosition[] {
                new SwitchPosition("1.0", "OFF", "3522"),
                new SwitchPosition("0.0", "ON", "3522")},
                "Caution Panel", "Alternator 1 Switch", "%0.1f"));
            AddFunction(new Switch(this, PWRPNL, "523", new SwitchPosition[] {
                new SwitchPosition("1.0", "OFF", "3523"),
                new SwitchPosition("0.0", "ON", "3523")},
                "Caution Panel", "Alternator 2 Switch", "%0.1f"));
            AddFunction(new Switch(this, PWRPNL, "524", new SwitchPosition[] {
                new SwitchPosition("1.0", "1", "3524"),
                new SwitchPosition("0.0", "OFF", "3524"),
                new SwitchPosition("-1.0", "2", "3524")},
                "Caution Panel", "Lights Test Switch", "%0.1f"));
            #endregion
            #region  ECM BOX
            AddFunction(new Switch(this, RWR, "194", new SwitchPosition[] {
                new SwitchPosition("1.0", "AUTO", "3194"),
                new SwitchPosition("0.5", "MANU", "3194"),
                new SwitchPosition("0.0", "ARRET", "3194")},
                "ECM Box", "Master Switch", "%0.1f"));
            AddFunction(new Switch(this, RWR, "195", new SwitchPosition[] {
                new SwitchPosition("1.0", "PTF", "3195"),
                new SwitchPosition("0.0", "C/C", "3195")},
                "ECM Box", "Dispensing Mode Switch", "%0.1f"));
            AddFunction(new Switch(this, RWR, "196", new SwitchPosition[] {
                new SwitchPosition("1.0", "OFF", "3196"),
                new SwitchPosition("0.0", "ON", "3196")},
                "ECM Box", "Lights Power Switch", "%0.1f"));
            AddFunction(new Axis(this, RWR, "3197", "197", 0.075d, 0d, 1d, "ECM Box", "Brightness Selector"));    // elements["PTN_197"] = default_axis_limited(_("ECM Box LCD Display Brightness"), devices.RWR, device_commands.Button_197, 197, 5, 0.5, false, 0)
            #endregion  
            #region Engine Sensors Panel
            AddFunction(new ScaledNetworkValue(this, "371", 1d, "Engine Sensors Panel", "Engine RPM (%) (Tens)", "Engine RPM (Tens).", "0 - 10", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "372", 1d, "Engine Sensors Panel", "Engine RPM (%) (Ones)", "Engine RPM (Ones).", "0 - 9", BindingValueUnits.Numeric));
            CalibrationPointCollectionDouble engineRPMScale = new CalibrationPointCollectionDouble(0.0d, 0.0d, 1d, 100d);
            AddFunction(new ScaledNetworkValue(this, "369", engineRPMScale, "Engine Sensors Panel", "Engine RPM Needle", "Engine RPM Needle.", "0 - 110", BindingValueUnits.RPMPercent));
            CalibrationPointCollectionDouble engineTempScale = new CalibrationPointCollectionDouble(0.0d, 0.0d, 1d, 10d);
            AddFunction(new ScaledNetworkValue(this, "370", engineTempScale, "Engine Sensors Panel", "Engine T7 Needle", "Engine Temp Needle.", "0 - 10", BindingValueUnits.Numeric));
            #endregion
            #region  Engine Start Panel
            AddFunction(new Switch(this, ENGPANEL, "645", new SwitchPosition[] {
                new SwitchPosition("0.0", "CLOSE", "3645"),
                new SwitchPosition("1.0", "OPEN", "3645")},
                "Engine Start Panel", "Engine Start Switch Guard", "%0.1f"));
            AddFunction(new PushButton(this, ENGPANEL, "3649", "649", "Engine Start Panel", "Engine Start Button"));
            AddFunction(new Switch(this, ENGPANEL, "646", new SwitchPosition[] {
                new SwitchPosition("0.0", "OFF", "3646"),
                new SwitchPosition("1.0", "ON", "3646")},
                "Engine Start Panel", "Starter Fuel Pump Switch", "%0.1f"));
            AddFunction(new Switch(this, INSTPANEL, "647", new SwitchPosition[] {
                new SwitchPosition("0.0", "OFF", "3647"),
                new SwitchPosition("1.0", "ON", "3647")},
                "Engine Start Panel", "Left Fuel Pump Switch", "%0.1f"));
            AddFunction(new Switch(this, INSTPANEL, "648", new SwitchPosition[] {
                new SwitchPosition("0.0", "OFF", "3648"),
                new SwitchPosition("1.0", "ON", "3648")},
                "Engine Start Panel", "Right Fuel Pump Switch", "%0.1f"));
            AddFunction(new Switch(this, ENGPANEL, "650", new SwitchPosition[] {
                new SwitchPosition("0.0", "VENT", "3650"),
                new SwitchPosition("0.5", "G", "3650"),
                new SwitchPosition("1.0", "D", "3650")},
                "Engine Start Panel", "Ignition Ventilation Selector Switch", "%0.1f"));
            AddFunction(new Switch(this, ENGPANEL, "651", new SwitchPosition[] {
                new SwitchPosition("0.0", "CLOSE", "3651"),
                new SwitchPosition("1.0", "OPEN", "3651")},
                "Engine Start Panel", "Fuel Shut-Off Switch Guard", "%0.1f"));
            AddFunction(new Switch(this, ENGPANEL, "652", new SwitchPosition[] {
                new SwitchPosition("1.0", "OFF", "3652"),
                new SwitchPosition("0.0", "ON", "3652")},
                "Engine Start Panel", "Fuel Shut-Off Switch", "%0.1f"));
            #endregion
            #region Fuel Panel
            AddFunction(new FlagValue(this, "198", "Fuel Panel", "Air Refueling", "Air Refueling"));
            AddFunction(new FlagValue(this, "362", "Fuel Panel", "left-rl", "Fuel Left RL"));
            AddFunction(new FlagValue(this, "363", "Fuel Panel", "center-rl", "Fuel Center RL"));
            AddFunction(new FlagValue(this, "364", "Fuel Panel", "right-rl", "Fuel Right RL"));
            AddFunction(new FlagValue(this, "365", "Fuel Panel", "left-av", "Fuel Left AV"));
            AddFunction(new FlagValue(this, "366", "Fuel Panel", "right-av", "Fuel Right AV"));
            AddFunction(new FlagValue(this, "367", "Fuel Panel", "left-v", "Fuel Left V"));
            AddFunction(new FlagValue(this, "368", "Fuel Panel", "right-v", "Fuel Right V"));
            AddFunction(new ScaledNetworkValue(this, "349", 1d, "Fuel Panel", "Internal Fuel Quantity (Thousands)", "Internal Fuel Quantity (Thousands).", "0-9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "350", 1d, "Fuel Panel", "Internal Fuel Quantity (Hundreds)", "Internal Fuel Quantity (Hundreds).", "0-9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "351", 1d, "Fuel Panel", "Internal Fuel Quantity (Tens)", "Internal Fuel Quantity (Tens).", "0-9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "352", 1d, "Fuel Panel", "Total Fuel Quantity (Thousands)", "Internal Fuel Quantity (Thousands).", "0-9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "353", 1d, "Fuel Panel", "Total Fuel Quantity (Hundreds)", "Internal Fuel Quantity (Hundreds).", "0-9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "354", 1d, "Fuel Panel", "Total Fuel Quantity (Tens)", "Internal Fuel Quantity (Tens).", "0-9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "358", 1.55d, "Fuel Panel", "Internal Fuel Quantity Needle", "Internal Fuel Quantity.", "0-7", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "359", 1.55d, "Fuel Panel", "Total Fuel Quantity Needle", "Total Fuel Quantity.", "0-7", BindingValueUnits.Numeric));
            AddFunction(Switch.CreateThreeWaySwitch(this, INSTPANEL, "3355", "355", "1.0", "+", "0.0", "Neutral", "-1.0", "-", "Fuel Panel", "Fuel Detotalizer Switch", "%0.1f"));
            AddFunction(new Switch(this, MISCPANELS, "357", new SwitchPosition[] {
                new SwitchPosition("0.0", "Open", "3357"),
                new SwitchPosition("1.0", "Close", "3357") },
                "Fuel Panel", "Fuel CrossFeed Switch", "%0.1f"));
            #endregion
            #region  HSI
            AddFunction(new Axis(this, NAVINST, "3340", "340", 0.015d, 0d, 1d, "HSI Panel", "VAD Selector"));    // elements["PTN_340"] = default_axis_cycle(_("HSI VAD Selector"),devices.NAVINST, device_commands.Button_340, 340)
            AddFunction(new Switch(this, NAVINST, "3341", new SwitchPosition[] {//need to be "341" but in that case, the needle doesn’t work anymore from DCS. 
                new SwitchPosition("0.0", "Cv/NAV", "3341"),                    //So better that way because the only problem is that doesn’t work from DCS to Helios
                new SwitchPosition("0.1", "NAV", "3341"),
                new SwitchPosition("0.2", "TAC", "3341"),
                new SwitchPosition("0.3", "VAD", "3341"),
                new SwitchPosition("0.4", "rho", "3341"),
                new SwitchPosition("0.5", "theta", "3341"),
                new SwitchPosition("0.6", "TEL", "3341")},
                "HSI Panel", "Mode Selector", "%0.1f"));
            AddFunction(new ScaledNetworkValue(this, "342", 1d, "HSI Panel", "Compass Rose", "Compass Rose.", "0 - 360", BindingValueUnits.Degrees, 0d, "%.4f"));
            AddFunction(new ScaledNetworkValue(this, "336", 1d, "HSI Panel", "Distance (Hundreds)", "Distance (Hundreds).", "0 - 9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "337", 1d, "HSI Panel", "Distance (Tens)", "Engine RPM (Tens).", "0 - 9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "338", 1d, "HSI Panel", "Distance (Ones)", "Distance (Ones).", "0 - 9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "339", 1d, "HSI Panel", "Distance (Decimals)", "Distance (Decimals).", "0 - 9", BindingValueUnits.Numeric));
            AddFunction(new FlagValue(this, "344", "HSI Panel", "Flag 1", "Flag 1"));
            AddFunction(new FlagValue(this, "345", "HSI Panel", "Flag 2", "Flag 2"));
            AddFunction(new FlagValue(this, "346", "HSI Panel", "Flag CAP", "Flag CAP"));
            AddFunction(new FlagValue(this, "343", "HSI Panel", "Flag distance", "Flag distance"));
            AddFunction(new ScaledNetworkValue(this, "333", 1d, "HSI Panel", "Direction Needle", "Direction Needle.", "0 - 360", BindingValueUnits.Degrees, 0d, "%.4f"));
            AddFunction(new ScaledNetworkValue(this, "334", 1d, "HSI Panel", "Big Needle", "Big Needle.", "0 - 360", BindingValueUnits.Degrees, 0d, "%.4f"));
            AddFunction(new ScaledNetworkValue(this, "335", 1d, "HSI Panel", "Small Needle", "Small Needle.", "0 - 360", BindingValueUnits.Degrees, 0d, "%.4f"));
            AddFunction(new ScaledNetworkValue(this, "341", 1d, "HSI Panel", "Mode Needle", "Mode Needle.", "0 - 360", BindingValueUnits.Degrees, 0d, "%.4f"));
            #endregion
            #region INS Panel
            AddFunction(new Switch(this, PCN_NAV, "627", new SwitchPosition[] {
                new SwitchPosition("0.0", "AR", "3627"),
                new SwitchPosition("0.1", "VEI", "3627"),
                new SwitchPosition("0.2", "CAL", "3627"),
                new SwitchPosition("0.3", "TST", "3627"),
                new SwitchPosition("0.4", "ALN", "3627"),
                new SwitchPosition("0.5", "ALCM", "3627"),
                new SwitchPosition("0.6", "NAV", "3627"),
                new SwitchPosition("0.7", "SEC", "3627")
                },
                "INS Panel", "Mode Selector", "%0.1f"));    // elements["PTN_627"] = multiposition_switch_limited(_("INS Mode Selector"), devices.PCN_NAV, device_commands.Button_627, 627, 8, 0.1, false, 0)
            AddFunction(new Switch(this, PCN_NAV, "629", new SwitchPosition[] {
                new SwitchPosition("0.0", "N", "3629"),
                new SwitchPosition("0.1", "STS", "3629"),
                new SwitchPosition("0.2", "DCI", "3629"),
                new SwitchPosition("0.3", "CRV", "3629"),
                new SwitchPosition("0.4", "MAIN", "3629"),
                },
                "INS Panel", "Operation Selector", "%0.1f"));    // elements["PTN_629"] = multiposition_switch_limited(_("INS Operational Mode"), devices.PCN_NAV, device_commands.Button_629, 629, 5, 0.1, true, 0)
            #endregion
            #region Landing Gear Panel
            AddFunction(new FlagValue(this, "410", "Landing Gear Panel", "A", "A Warnlamp"));
            AddFunction(new FlagValue(this, "411", "Landing Gear Panel", "F", "F Warnlamp"));
            AddFunction(new FlagValue(this, "412", "Landing Gear Panel", "DIRAV", "DIRA Warnlamp, blau"));
            AddFunction(new FlagValue(this, "413", "Landing Gear Panel", "FREIN", "FREIN"));
            AddFunction(new FlagValue(this, "414", "Landing Gear Panel", "CROSS", "Cross"));
            AddFunction(new FlagValue(this, "415", "Landing Gear Panel", "SPAD", "SPAD"));
            AddFunction(new FlagValue(this, "416", "Landing Gear Panel", "BIP", "Red Warnlamp under BIP"));
            AddFunction(new FlagValue(this, "417", "Landing Gear Panel", "left-gear", "Left Gear"));
            AddFunction(new FlagValue(this, "418", "Landing Gear Panel", "nose-gear", "Nose Gear"));
            AddFunction(new FlagValue(this, "419", "Landing Gear Panel", "right-gear", "Right Gear"));
            AddFunction(new Switch(this, INSTPANEL, "404", new SwitchPosition[] {
                new SwitchPosition("0.0", "UP", "3404"),
                new SwitchPosition("1.0", "DOWN", "3404") },
                "Landing Gear Panel", "Landing Gear Lever", "%0.1f"));
            AddFunction(new Switch(this, PCA_PPA, "463", new SwitchPosition[] {
                new SwitchPosition("1.0", "SECU", "3463"),
                new SwitchPosition("0.0", "ARMED", "3463") },
                "Landing Gear Panel", "Gun Arming Switch", "%0.1f"));
            AddFunction(new Switch(this, ENGPANEL, "420", new SwitchPosition[] {
                new SwitchPosition("0.0", "CLOSE", "3420"),
                new SwitchPosition("1.0", "OPEN", "3420") },
                "Landing Gear Panel", "Fly By Wire Gain Switch Guard", "%0.1f"));
            AddFunction(new Switch(this, ENGINE, "421", new SwitchPosition[] {
                new SwitchPosition("0.0", "NORM", "3421"),
                new SwitchPosition("1.0", "GAIN CDVE", "3421") },
                "Landing Gear Panel", "Fly by Wire Gain Mode Switch", "%0.1f"));
            AddFunction(new Switch(this, ENGINE, "422", new SwitchPosition[] {
                new SwitchPosition("0.0", "AA", "3422"),
                new SwitchPosition("1.0", "CHARGES", "3422") },
                "Landing Gear Panel", "Fly by Wire G Limiter Switch", "%0.1f"));
            AddFunction(new Switch(this, INSTPANEL, "408", new SwitchPosition[] {
                new SwitchPosition("0.0", "ON", "3408"),
                new SwitchPosition("1.0", "OFF", "3408") },
                "Landing Gear Panel", "Emergency Landing Gear Lever", "%0.1f"));
            AddFunction(new PushButton(this, PCA_PPA, "3409", "409", "Landing Gear Panel", "Emergency Jettison Lever"));
            #endregion
            #region MCL Panel
            AddFunction(new FlagValue(this, "199", "Master Caution Lights Panel", "Panne Yellow", "Master-warning"));
            AddFunction(new FlagValue(this, "200", "Master Caution Lights Panel", "Panne Red", "Master-caution"));
            AddFunction(new PushButton(this, SYSLIGHTS, "3191", "191", "Master Caution Lights Panel", "Acknowledge alarms"));
            #endregion
            #region Miscellaneous Panels and indicators
            AddFunction(new FlagValue(this, "229", "RWR Panel", "RWR V", "RWR V"));
            AddFunction(new FlagValue(this, "230", "RWR Panel", "RWR BR", "RWR BR"));
            AddFunction(new FlagValue(this, "231", "RWR Panel", "RWR DA", "RWR DA"));
            AddFunction(new FlagValue(this, "232", "RWR Panel", "RWR D2M", "RWR D2M"));
            AddFunction(new FlagValue(this, "233", "RWR Panel", "RWR LL", "RWR LL"));
            AddFunction(new FlagValue(this, "373", "Post Combustion Indicator Panel", "pc", "Post Combustion"));
            AddFunction(new FlagValue(this, "374", "Fire Warning Panel", "fire-warning-engine-chamber", "Fire Warning Engine Chamber"));
            AddFunction(new FlagValue(this, "375", "Fire Warning Panel", "fire-warning-afterburner-chamber", "Fire Warning Afterburner Chamber"));
            AddFunction(new FlagValue(this, "376", "Demar Indicator Panel", "demar", "Start-up"));
            AddFunction(new ScaledNetworkValue(this, "331", 3.7d, "AOA Panel", "AOA Needle", "Angle Of Attack Needle.", "0-7", BindingValueUnits.Numeric, 0.08d, "%.4f"));
            #endregion
            #region  PCA/PPA
            AddFunction(new Switch(this, PCA_PPA, "234", new SwitchPosition[] {
                new SwitchPosition("1.0", "ON", "3234"),
                new SwitchPosition("0.0", "OFF", "3234")},
                "PCA Panel", "Master Arm Switch", "%0.1f"));
            AddFunction(new Switch(this, PCA_PPA, "248", new SwitchPosition[] {
                new SwitchPosition("0.0", "CLOSE", "3248"),
                new SwitchPosition("1.0", "OPEN", "3248")},
                "PCA Panel", "Selective Jettison Switch Guard", "%0.1f"));
            AddFunction(new Switch(this, PCA_PPA, "249", new SwitchPosition[] {
                new SwitchPosition("0.0", "OFF", "3249"),
                new SwitchPosition("1.0", "ON", "3249")},
                "PCA Panel", "Selective Jettison Switch", "%0.1f"));
            AddFunction(new PushButton(this, PCA_PPA, "3235", "235", "PCA Panel", "Targeting Mode Selection"));
            AddFunction(new PushButton(this, PCA_PPA, "3237", "237", "PCA Panel", "Master Mode Selection"));
            AddFunction(new PushButton(this, PCA_PPA, "3239", "239", "PCA Panel", "Approach Mode Selection"));
            AddFunction(new PushButton(this, PCA_PPA, "3241", "241", "PCA Panel", "Fligt Plan Route Selection"));
            AddFunction(new PushButton(this, PCA_PPA, "3243", "243", "PCA Panel", "INS Calibration"));
            AddFunction(new PushButton(this, PCA_PPA, "3245", "245", "PCA Panel", "Gun Mode Selector"));
            AddFunction(new PushButton(this, PCA_PPA, "3250", "250", "PCA Panel", "Weapon Store Selector 1"));
            AddFunction(new PushButton(this, PCA_PPA, "3253", "253", "PCA Panel", "Weapon Store Selector 2"));
            AddFunction(new PushButton(this, PCA_PPA, "3256", "256", "PCA Panel", "Weapon Store Selector 3"));
            AddFunction(new PushButton(this, PCA_PPA, "3259", "259", "PCA Panel", "Weapon Store Selector 4"));
            AddFunction(new PushButton(this, PCA_PPA, "3262", "262", "PCA Panel", "Weapon Store Selector 5"));
            AddFunction(new FlagValue(this, "236", "PCA Panel", "TMS S", "TMS S"));
            AddFunction(new FlagValue(this, "238", "PCA Panel", "MMS", "MMS"));
            AddFunction(new FlagValue(this, "240", "PCA Panel", "AMS", "AMS"));
            AddFunction(new FlagValue(this, "242", "PCA Panel", "FPRS", "FPRS"));
            AddFunction(new FlagValue(this, "244", "PCA Panel", "INS C", "INS C"));
            AddFunction(new FlagValue(this, "251", "PCA Panel", "WSS1 S", "WSS1 S"));
            AddFunction(new FlagValue(this, "252", "PCA Panel", "WSS1 P", "WSS1 P"));
            AddFunction(new FlagValue(this, "254", "PCA Panel", "WSS2 S", "WSS2 S"));
            AddFunction(new FlagValue(this, "255", "PCA Panel", "WSS2 P", "WSS2 P"));
            AddFunction(new FlagValue(this, "257", "PCA Panel", "WSS3 S", "WSS3 S"));
            AddFunction(new FlagValue(this, "258", "PCA Panel", "WSS3 P", "WSS3 P"));
            AddFunction(new FlagValue(this, "260", "PCA Panel", "WSS4 S", "WSS4 S"));
            AddFunction(new FlagValue(this, "261", "PCA Panel", "WSS4 P", "WSS4 P"));
            AddFunction(new FlagValue(this, "263", "PCA Panel", "WSS5 S", "WSS5 S"));
            AddFunction(new FlagValue(this, "264", "PCA Panel", "WSS5 P", "WSS5 P"));
            AddFunction(new FlagValue(this, "246", "PCA Panel", "KL1", "KL1"));
            AddFunction(new FlagValue(this, "247", "PCA Panel", "KL2", "KL2"));
            AddFunction(new Text(this, "2060", "PCA Panel", "PCA Upper Display", "Display Upper Line"));
            AddFunction(new Text(this, "2061", "PCA Panel", "PCA Lower Display", "Display Lower Line"));

            AddFunction(new PushButton(this, PCA_PPA, "3266", "266", "PPA Panel", "S530 Missile Enabler Button"));
            AddFunction(new PushButton(this, PCA_PPA, "3269", "269", "PPA Panel", "Missile Fire Mode Selector"));
            AddFunction(new PushButton(this, PCA_PPA, "3272", "272", "PPA Panel", "Magic II Missile Enabler Button")); 
            AddFunction(new PushButton(this, PCA_PPA, "3279", "279", "PPA Panel", "Guns/Rockets/Missiles Firing Mode Selector"));
            AddFunction(new Switch(this, PCA_PPA, "265", new SwitchPosition[] {
                new SwitchPosition("-1.0", "Gauche","3265"),
                new SwitchPosition("0.0", "Droite","3265"),
                new SwitchPosition("1.0", "Auto","3265")},
                "PPA Panel", "Missile Selector Switch", "%0.1f"));
            AddFunction(new Switch(this, PCA_PPA, "275", new SwitchPosition[] {
                new SwitchPosition("-1.0", "TEST","3275"),
                new SwitchPosition("0.0", "NEUTRE","3275"),
                new SwitchPosition("1.0", "PRES","3275")},
                "PPA Panel", "Test Switch", "%0.1f"));
            AddFunction(new Switch(this, PCA_PPA, "276", new SwitchPosition[] {
                new SwitchPosition("0.0", "INST.","3276"),
                new SwitchPosition("0.5", "RET.","3276"),
                new SwitchPosition("1.0", "INERT.","3276")},
                "PPA Panel", "Bomb Fuse Selector", "%0.1f")); 
            AddFunction(new Switch(this, PCA_PPA, "277", new SwitchPosition[] {
                new SwitchPosition("1.0", "-","3277"),
                new SwitchPosition("0.0", "+","3277"),
                new SwitchPosition("-1.0", "Neutre","3277")},
                "PPA Panel", "Release Quantity Selector", "%0.1f"));
            AddFunction(new Switch(this, PCA_PPA, "278", new SwitchPosition[] {
                new SwitchPosition("1.0", "-","3278"),
                new SwitchPosition("0.0", "+","3278"),
                new SwitchPosition("-1.0", "Neutre","3278")},
                "PPA Panel", "Bomb Drop Interval", "%0.1f"));
            AddFunction(new FlagValue(this, "267", "PPA Panel", "S530D P", "S530D P"));
            AddFunction(new FlagValue(this, "268", "PPA Panel", "S530D MIS", "S530D MIS"));
            AddFunction(new FlagValue(this, "270", "PPA Panel", "Missile AUT Mode", "Missile AUT Mode"));
            AddFunction(new FlagValue(this, "271", "PPA Panel", "Missile MAN Mode", "Missile MAN Mode"));
            AddFunction(new FlagValue(this, "273", "PPA Panel", "MAGIC P", "MAGIC P"));
            AddFunction(new FlagValue(this, "274", "PPA Panel", "MAGIC MAG", "MAGIC MAG"));
            AddFunction(new FlagValue(this, "280", "PPA Panel", "TOT Firing Mode", "TOT Firing Mode"));
            AddFunction(new FlagValue(this, "281", "PPA Panel", "PAR Firing Mode", "PAR Firing Mode"));
            AddFunction(new Text(this, "2065", "PPA Panel", "PPA Display Quantity", "Display Line for the PPA Quantity"));
            AddFunction(new Text(this, "2066", "PPA Panel", "PPA Display Interval", "Display Line for the PPA Interval"));


            #endregion
            #region  PCN
            AddFunction(new FlagValue(this, "564", "PCN Panel", "PRET", "PRET"));
            AddFunction(new FlagValue(this, "565", "PCN Panel", "ALN", "ALN"));
            AddFunction(new FlagValue(this, "566", "PCN Panel", "MIP", "MIP"));
            AddFunction(new FlagValue(this, "595", "PCN Panel", "EFF", "EFF"));
            AddFunction(new FlagValue(this, "597", "PCN Panel", "INS", "INS"));
            AddFunction(new FlagValue(this, "567", "PCN Panel", "NDEG", "NDEG"));
            AddFunction(new FlagValue(this, "568", "PCN Panel", "SEC", "SEC"));
            AddFunction(new FlagValue(this, "569", "PCN Panel", "UNI", "UNI"));
            AddFunction(new FlagValue(this, "669", "PCN Panel", "M91", "M91"));
            AddFunction(new FlagValue(this, "670", "PCN Panel", "M92", "M92"));
            AddFunction(new FlagValue(this, "671", "PCN Panel", "M93", "M93"));
            AddFunction(new PushButton(this, PCN_NAV, "3570", "570", "PCN Panel", "PREP Button"));
            AddFunction(new FlagValue(this, "571", "PCN Panel", "PREP", "PREP"));
            AddFunction(new PushButton(this, PCN_NAV, "3572", "572", "PCN Panel", "DEST Button"));
            AddFunction(new FlagValue(this, "573", "PCN Panel", "DEST", "DEST"));
            //The ENC button has been removed from the aircraft
            //AddFunction(new PushButton(this, PCN_NAV, "3667", "667", "PCN Panel", "Offset Waypoint/Target Button"));
            //AddFunction(new FlagValue(this, "668", "PCN Panel", "Offset Waypoint/Target", "ENC"));
            AddFunction(new PushButton(this, PCN_NAV, "3578", "578", "PCN Panel", "INS Update Button"));
            AddFunction(new FlagValue(this, "579", "PCN Panel", "INS Update", "REC"));
            AddFunction(new PushButton(this, PCN_NAV, "3580", "580", "PCN Panel", "Validate Data Entry Button"));
            AddFunction(new FlagValue(this, "581", "PCN Panel", "Validate Data Entry", "VAL"));
            AddFunction(new PushButton(this, PCN_NAV, "3582", "582", "PCN Panel", "Marq Position Button"));
            AddFunction(new FlagValue(this, "583", "PCN Panel", "Marq Position", "MRQ"));
            AddFunction(new PushButton(this, PCN_NAV, "3576", "576", "PCN Panel", "AUTO Navigation Button"));
            AddFunction(new FlagValue(this, "577", "PCN Panel", "AUTO Navigation", "BAD"));
            AddFunction(new Switch(this, PCN_NAV, "574", new SwitchPosition[] {
                new SwitchPosition("0.0", "TR/VS", "3574"),
                new SwitchPosition("0.1", "D/RLT", "3574"),
                new SwitchPosition("0.2", "CP/PD", "3574"),
                new SwitchPosition("0.3", "ALT", "3574"),
                new SwitchPosition("0.4", "L/G", "3574"),
                new SwitchPosition("0.5", "RT/TD", "3574"),
                new SwitchPosition("0.6", "dL/dG", "3574"),
                new SwitchPosition("0.7", "dALT", "3574"),
                new SwitchPosition("0.8", "P/t", "3574"),
                new SwitchPosition("0.9", "DEC", "3574"),
                new SwitchPosition("1.0", "DV/FV", "3574")},
                "PCN Panel", "INS Parameter Selector", "%0.2f"));
            AddFunction(new Axis(this, SYSLIGHTS, "3575", "575", 0.15d, 0d, 1d, "PCN Panel", "Light Brightnes Control/Test"));
            AddFunction(new PushButton(this, PCN_NAV, "3584", "584", "PCN Panel", "INS Button 1"));
            AddFunction(new PushButton(this, PCN_NAV, "3585", "585", "PCN Panel", "INS Button 2"));
            AddFunction(new PushButton(this, PCN_NAV, "3586", "586", "PCN Panel", "INS Button 3"));
            AddFunction(new PushButton(this, PCN_NAV, "3587", "587", "PCN Panel", "INS Button 4"));
            AddFunction(new PushButton(this, PCN_NAV, "3588", "588", "PCN Panel", "INS Button 5"));
            AddFunction(new PushButton(this, PCN_NAV, "3589", "589", "PCN Panel", "INS Button 6"));
            AddFunction(new PushButton(this, PCN_NAV, "3590", "590", "PCN Panel", "INS Button 7"));
            AddFunction(new PushButton(this, PCN_NAV, "3591", "591", "PCN Panel", "INS Button 8"));
            AddFunction(new PushButton(this, PCN_NAV, "3592", "592", "PCN Panel", "INS Button 9"));
            AddFunction(new PushButton(this, PCN_NAV, "3593", "593", "PCN Panel", "INS Button 0"));
            AddFunction(new PushButton(this, PCN_NAV, "3594", "594", "PCN Panel", "EFF Button"));
            AddFunction(new PushButton(this, PCN_NAV, "3596", "596", "PCN Panel", "INS Button"));

            AddFunction(new Text(this, "2068", "PCN Panel", "PCN Latitude Display", "Display Line for PCN Latitude"));
            AddFunction(new Text(this, "2069", "PCN Panel", "PCN Left Points Position", "Position of Points for PCN Left"));
            AddFunction(new Text(this, "2070", "PCN Panel", "PCN Longitude Display", "Display Line for PCN Longitude"));
            AddFunction(new Text(this, "2071", "PCN Panel", "PCN Right Points Position", "Position of Points for PCN Right"));

            AddFunction(new NetworkValue(this, "2072", "PCN Panel", "PCN North", "North Indicator on the PCN display", "Boolean True/False", BindingValueUnits.Boolean, null));
            AddFunction(new NetworkValue(this, "2073", "PCN Panel", "PCN South", "South Indicator on the PCN display", "Boolean True/False", BindingValueUnits.Boolean, null));
            AddFunction(new NetworkValue(this, "2074", "PCN Panel", "PCN East", "East Indicator on the PCN display", "Boolean True/False", BindingValueUnits.Boolean, null));
            AddFunction(new NetworkValue(this, "2075", "PCN Panel", "PCN West", "West Indicator on the PCN display", "Boolean True/False", BindingValueUnits.Boolean, null));
            AddFunction(new NetworkValue(this, "2076", "PCN Panel", "PCN Left Plus", "Plus Indicator on the Left PCN display", "Boolean True/False", BindingValueUnits.Boolean, null));
            AddFunction(new NetworkValue(this, "2077", "PCN Panel", "PCN Left Minus", "Minus Indicator on the Left PCN display", "Boolean True/False", BindingValueUnits.Boolean, null));
            AddFunction(new NetworkValue(this, "2078", "PCN Panel", "PCN Right Plus", "Plus Indicator on the Right PCN display", "Boolean True/False", BindingValueUnits.Boolean, null));
            AddFunction(new NetworkValue(this, "2079", "PCN Panel", "PCN Right Minus", "Minus Indicator on the Right PCN display", "Boolean True/False", BindingValueUnits.Boolean, null));
            AddFunction(new Text(this, "2080", "PCN Panel", "PCN Lower Left Display", "Display Lower Line Left Side"));
            AddFunction(new Text(this, "2081", "PCN Panel", "PCN Lower Right Display", "Display Lower Line Right Side"));

            #endregion
            #region TACAN Panel
            AddFunction(new Axis(this, TACAN, "3625", "625", 0.1d, 0d, 1d, "Tacan Panel", "Channel 1 Selector"));
            AddFunction(new Axis(this, TACAN, "3623", "623", 0.077d, 0d, 0.923d, "Tacan Panel", "Channel 10 Selector"));
            AddFunction(new ScaledNetworkValue(this, "622", 1d, "Tacan Panel", "Channel output for display (Ones)", "Current channel (Ones)", "(0-9)", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "621", 1d, "Tacan Panel", "Channel output for display (Tens)", "Current channel (Tens)", "(0-12)", BindingValueUnits.Numeric, 0.3d, "%.1f"));
            AddFunction(new ScaledNetworkValue(this, "620", 1d, "Tacan Panel", "X/Y Drum", "X/Y Mode", "(1 - 2)", BindingValueUnits.Numeric));
            AddFunction(new Switch(this, TACAN, TACAN_XY_SELECTOR, new SwitchPosition[] {
                new SwitchPosition("0.0", "X", CMD + TACAN_XY_SELECTOR),
                new SwitchPosition("1.0", "Y", CMD + TACAN_XY_SELECTOR) },
                "Tacan Panel", "X/Y Selector", "%0.1f"));
            AddFunction(new Switch(this, TACAN, TACAN_MODE_SELECTOR, new SwitchPosition[] {
                new SwitchPosition("0.0", "OFF", CMD + TACAN_MODE_SELECTOR),
                new SwitchPosition("0.3", "REC", CMD + TACAN_MODE_SELECTOR),
                new SwitchPosition("0.7", "T/R", CMD + TACAN_MODE_SELECTOR),
                new SwitchPosition("1.0", "A/A", CMD + TACAN_MODE_SELECTOR) },
                "Tacan Panel", "Mode Selector", " %0.1f"));
            #endregion
            #region Test Panel
            AddFunction(new FlagValue(this, "510", "Test Panel", "ELEC", "ELEC indicator"));
            AddFunction(new FlagValue(this, "511", "Test Panel", "HYD", "HYD indicator"));
            AddFunction(new FlagValue(this, "512", "Test Panel", "Test Fail", "Test fail indicator"));
            AddFunction(new FlagValue(this, "513", "Test Panel", "Test Ok", "Test ok indicator"));
            AddFunction(new Switch(this, AFCS, "514", new SwitchPosition[] {
                new SwitchPosition("0.0", "CLOSE", "3514"),
                new SwitchPosition("1.0", "OPEN", "3514"), },
                "Test Panel", "Autopilot Test Guard", "%0.1f"));
            AddFunction(new Switch(this, AFCS, "515", new SwitchPosition[] {
                new SwitchPosition("1.0", "ON", "3515"),
                new SwitchPosition("0.0", "OFF", "3515"), },
                "Test Panel", "Autopilot Test Switch", "%0.1f"));
            AddFunction(new Switch(this, AFCS, "516", new SwitchPosition[] {
                new SwitchPosition("0.0", "CLOSE", "3516"),
                new SwitchPosition("1.0", "OPEN", "3516"), },
                "Test Panel", "FBW Test Guard", "%0.1f"));
            AddFunction(new Switch(this, AFCS, "517", new SwitchPosition[] {
                new SwitchPosition("0.0", "C", "3517"),
                new SwitchPosition("0.5", "OFF", "3517"),
                new SwitchPosition("1.0", "TEST", "3517"), },
                "Test Panel", "FBW Test Switch", "%0.1f"));
            AddFunction(new Switch(this, AFCS, "479", new SwitchPosition[] {
                new SwitchPosition("0.0", "CLOSE", "3479"),
                new SwitchPosition("1.0", "OPEN", "3479"), },
                "Test Panel", "FBW Channel 5 Guard", "%0.1f"));
            AddFunction(new Switch(this, AFCS, "480", new SwitchPosition[] {
                new SwitchPosition("0.0", "ON", "3480"),
                new SwitchPosition("1.0", "OFF", "3480"), },
                "Test Panel", "FBW Channel 5 Switch", "%0.1f"));
            #endregion
            #region VOR.ILS Panel
            AddFunction(new ScaledNetworkValue(this, "615", 1d, "VOR/ILS Panel", "Channel output for display (Decimal Ones)", "Current channel", "0 - 9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "614", 1d, "VOR/ILS Panel", "Channel output for display (Decimal Tens)", "Current channel", "0 - 9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "613", 1d, "VOR/ILS Panel", "Channel output for display (Ones)", "Current channel", "0 - 9", BindingValueUnits.Numeric));
            AddFunction(new ScaledNetworkValue(this, "612", 1d, "VOR/ILS Panel", "Channel output for display (Tens)", "Current channel", "0 - 1", BindingValueUnits.Numeric));
            AddFunction(new Axis(this, VORILS, CMD + VORILS_FREQUENCY_CHANGE_WHOLE, VORILS_FREQUENCY_CHANGE_WHOLE, 0.1d, 0.0d, 1.0d, "VOR/ILS Panel", "Frequency Change whole"));
            AddFunction(new Axis(this, VORILS, CMD + VORILS_FREQUENCY_CHANGE_DECIMAL, VORILS_FREQUENCY_CHANGE_DECIMAL, 0.05d, 0.0d, 1.0d, "VOR/ILS Panel", "Frequency Change Decimal"));
            AddFunction(new Switch(this, VORILS, VORILS_POWER, new SwitchPosition[] {
                new SwitchPosition("0.0", "ON", CMD + VORILS_POWER),
                new SwitchPosition("1.0", "OFF", CMD + VORILS_POWER)},
                "VOR/ILS Panel", "Power Selector", "%0.1f"));
            AddFunction(new Switch(this, VORILS, "619", new SwitchPosition[] {
                new SwitchPosition("0.0", "HG", "3619"),
                new SwitchPosition("1.0", "BD", "3619")},
                "VOR/ILS Panel", "Mode Selector", "%0.1f"));
            #endregion
            #region Indicators
            // !!!! Any duplicate "name" values in a function will cause Helios to go bang.  Make sure that when you change the name, that it is unique
            AddFunction(new FlagValue(this, "185", "Indicators", "Indicators 185", "LIM, MIP,"));
            AddFunction(new FlagValue(this, "186", "Indicators", "Indicators 186", "IFF, MIP, Acc"));
            AddFunction(new FlagValue(this, "187", "Indicators", "Indicators 187", "LED green, ADI"));
            AddFunction(new FlagValue(this, "188", "Indicators", "Indicators 188", "LED green, ADI"));
            AddFunction(new FlagValue(this, "321", "Indicators", "Indicators 321", "ADI ILS light"));
            AddFunction(new FlagValue(this, "283", "Indicators", "Indicators 283", "AUTOPILOT P"));
            AddFunction(new FlagValue(this, "284", "Indicators", "Indicators 284", "AUTOPILOT A"));
            AddFunction(new FlagValue(this, "286", "Indicators", "Indicators 286", "AUTOPILOT Alt 1"));
            AddFunction(new FlagValue(this, "287", "Indicators", "Indicators 287", "AUTOPILOT blank Alt"));
            AddFunction(new FlagValue(this, "289", "Indicators", "Indicators 289", "AUTOPILOT Alt 2"));
            AddFunction(new FlagValue(this, "290", "Indicators", "Indicators 290", "AUTOPILOT AFF"));
            AddFunction(new FlagValue(this, "292", "Indicators", "Indicators 292", "AUTOPILOT blank1"));
            AddFunction(new FlagValue(this, "293", "Indicators", "Indicators 293", "AUTOPILOT blank2"));
            AddFunction(new FlagValue(this, "295", "Indicators", "Indicators 295", "AUTOPILOT left"));
            AddFunction(new FlagValue(this, "296", "Indicators", "Indicators 296", "AUTOPILOT blank L"));
            AddFunction(new FlagValue(this, "297", "Indicators", "Indicators 297", "AUTOPILOT G"));
            AddFunction(new FlagValue(this, "298", "Indicators", "Indicators 298", "AUTOPILOT blank G"));
            AddFunction(new FlagValue(this, "405", "Indicators", "Indicators 405", "Gearhandle Innenleuchte, red"));
            AddFunction(new FlagValue(this, "677", "Indicators", "Indicators 677", "COM left green lamp"));
            AddFunction(new FlagValue(this, "519", "Indicators", "Indicators 519", "Oxy flow lamp"));
            AddFunction(new FlagValue(this, "490", "Indicators", "Indicators 490", "Left consule VAL"));
            AddFunction(new FlagValue(this, "492", "Indicators", "Indicators 492", "Left consule A"));
            AddFunction(new FlagValue(this, "494", "Indicators", "Indicators 494", "Left consule DEC"));
            AddFunction(new FlagValue(this, "496", "Indicators", "Indicators 496", "Left consule VISU"));
            AddFunction(new FlagValue(this, "505", "Indicators", "Indicators 505", "Left consule PSIC"));
            AddFunction(new FlagValue(this, "632", "Indicators", "Indicators 632", "TACAN C"));
            AddFunction(new FlagValue(this, "634", "Indicators", "Indicators 634", "TACAN F"));
            AddFunction(new FlagValue(this, "675", "Indicators", "Indicators 675", "COM Panel, lamp red"));
            AddFunction(new FlagValue(this, "676", "Indicators", "Indicators 676", "COM Panel, lamp red, over COM"));
            #endregion
            #region  Infligt Engine Panel
            AddFunction(new PushButton(this, ENGPANEL, "3468", "468", "Engine Start Panel", "Engine In-Flight Start Switch"));    // elements["PTN_468"] = default_2_position_tumb(_("Engine In-Flight Start Switch"), devices.ENGPANEL, device_commands.Button_468, 468, 0, 1)
            AddFunction(new PushButton(this, ENGPANEL, "3467", "467", "Engine Start Panel", "Engine Shutdown Button"));    // elements["PTN_467"] = default_button(_("Engine Shutdown Button"), devices.ENGPANEL, device_commands.Button_467, 467)
            AddFunction(new PushButton(this, INSTPANEL, "3477", "477", "Engine Start Panel", "Fuel Dump Switch Cover"));    // elements["PTN_477"] = default_2_position_tumb(_("Fuel Dump Switch Cover"), devices.INSTPANEL, device_commands.Button_477, 477)
            AddFunction(new PushButton(this, INSTPANEL, "3478", "478", "Engine Start Panel", "Fuel Dump Switch"));    // elements["PTN_478"] = default_2_position_tumb(_("Fuel Dump Switch"), devices.INSTPANEL, device_commands.Button_478, 478)
            AddFunction(new PushButton(this, INSTPANEL, "3471", "471", "Engine Start Panel", "A/B Emergency Cutoff Switch Cover"));    // elements["PTN_471"] = default_2_position_tumb(_("A/B Emergency Cutoff Switch Cover"), devices.INSTPANEL, device_commands.Button_471, 471)
            AddFunction(new PushButton(this, INSTPANEL, "3472", "472", "Engine Start Panel", "A/B Emergency Cutoff Switch"));    // elements["PTN_472"] = default_2_position_tumb(_("A/B Emergency Cutoff Switch"), devices.INSTPANEL, device_commands.Button_472, 472)
            AddFunction(new PushButton(this, ENGPANEL, "3464", "464", "Engine Start Panel", "Emergency Throttle Cover"));    // elements["PTN_464"] = default_2_position_tumb(_("Emergency Throttle Cover"), devices.ENGPANEL, device_commands.Button_464, 464)
            AddFunction(new Axis(this, ENGPANEL, "3465", "465", 0.15d, 0d, 1d, "Engine Start Panel", "Emergency Throttle Handle"));    // elements["PTN_465"] = default_axis_limited(_("Emergency Throttle Handle"),devices.ENGPANEL,device_commands.Button_465,465, 0.8, 0.5, true, false, {0.0, 1.0})
            AddFunction(new PushButton(this, ENGPANEL, "3473", "473", "Engine Start Panel", "Secondary Oil Control Cover"));    // elements["PTN_473"] = default_2_position_tumb(_("Secondary Oil Control Cover"), devices.ENGPANEL, device_commands.Button_473, 473)
            AddFunction(new PushButton(this, ENGPANEL, "3474", "474", "Engine Start Panel", "Secondary Oil Control Switch"));    // elements["PTN_474"] = default_2_position_tumb(_("Secondary Oil Control Switch"), devices.ENGPANEL, device_commands.Button_474, 474)
            AddFunction(new PushButton(this, ENGPANEL, "3475", "475", "Engine Start Panel", "Engine Emergency Control Cover"));    // elements["PTN_475"] = default_2_position_tumb(_("Engine Emergency Control Cover"), devices.ENGPANEL, device_commands.Button_475, 475)
            AddFunction(new Switch(this, ENGPANEL, "476", new SwitchPosition[] { }, "Engine Start Panel", "Engine Emergency Control Switch", "%0.1f"));    // elements["PTN_476"] = multiposition_switch_limited(_("Engine Emergency Control Switch"), devices.ENGPANEL, device_commands.Button_476, 476,3, 0.5, true, 0)
            #endregion  
            #region U/VHF Panel
            AddFunction(new ScaledNetworkValue(this, UVHF_PRESET_DISPLAY, 0d, "U/VHF", "Preset output for display", "Current preset channel", "use rotary encoder with initial 0, min0, max 20, step 0.1", BindingValueUnits.Numeric, 0d, "%.4f"));
            AddFunction(new Axis(this, UVHF, UVHF_PRESET_KNOB, CMD + UVHF_PRESET_KNOB, 0.05d, 0d, 1.0d, "U/VHF", "Preset frequency change"));
            AddFunction(new Text(this, "2062", "U/VHF", "VHF Comm Information", "Display Line for the VHF Radio"));
            AddFunction(new Text(this, "2064", "U/VHF", "UHF Upper Comm Information", "Upper Display Line for the UHF Radio"));
            AddFunction(new Text(this, "2063", "U/VHF", "UHF Lower Comm Information", "Lower Display Line for the UHF Radio"));


            //            AddFunction(new RotaryEncoder(this, UVHF, UVHF_PRESET_KNOB, CMD + UVHF_PRESET_KNOB, 0.1d, "U/VHF", "Preset frequency change"));
            /*            AddFunction(new Switch(this, UVHF, UVHF_CHANNEL, new SwitchPosition[] { 
                            new SwitchPosition("0.05", "Channel 1", CMD+UVHF_CHANNEL), new SwitchPosition("0.1", "Channel 2", CMD+UVHF_CHANNEL), new SwitchPosition("0.15", "Channel 3", CMD+UVHF_CHANNEL),
                            new SwitchPosition("0.2", "Channel 4", CMD+UVHF_CHANNEL), new SwitchPosition("0.25", "Channel 5", CMD+UVHF_CHANNEL), new SwitchPosition("0.3", "Channel 6", CMD+UVHF_CHANNEL),
                            new SwitchPosition("0.35", "Channel 7", CMD+UVHF_CHANNEL), new SwitchPosition("0.4", "Channel 8", CMD+UVHF_CHANNEL) , new SwitchPosition("0.45", "Channel 9", CMD+UVHF_CHANNEL),
                            new SwitchPosition("0.5", "Channel 10", CMD+UVHF_CHANNEL), new SwitchPosition("0.55", "Channel 11", CMD+UVHF_CHANNEL) , new SwitchPosition("0.66", "Channel 12", CMD+UVHF_CHANNEL),
                            new SwitchPosition("0.65", "Channel 13", CMD+UVHF_CHANNEL), new SwitchPosition("0.7", "Channel 14", CMD+UVHF_CHANNEL) , new SwitchPosition("0.75", "Channel 15", CMD+UVHF_CHANNEL),
                            new SwitchPosition("0.8", "Channel 16", CMD+UVHF_CHANNEL), new SwitchPosition("0.85", "Channel 17", CMD+UVHF_CHANNEL) , new SwitchPosition("0.9", "Channel 18", CMD+UVHF_CHANNEL),
                            new SwitchPosition("0.95", "Channel 19", CMD+UVHF_CHANNEL), new SwitchPosition("1", "Channel 20", CMD+UVHF_CHANNEL)}, "Radio Panel", "U/VHF Channel Selector", "%0.2f"));
                            */
            #endregion
            #region  HUD/VTB
            AddFunction(new Switch(this, VTH_VTB, "201", new SwitchPosition[] { }, "HUD/VTB", "HUD Power Switch", "%0.1f"));    // elements["PTN_201"] = multiposition_switch_limited(_("HUD Power Switch"), devices.VTH_VTB, device_commands.Button_201, 201, 3, 0.5, false, 0)
            //AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3203", "203", "HUD/VTB", "HUD Declutter Switch", "%0.1f"));    // elements["PTN_203"] = default_2_way_spring_switch(_("HUD Declutter Switch"), devices.VTH_VTB, device_commands.Button_203, 203, true)
            AddFunction(new Switch(this, VTH_VTB, "204", new SwitchPosition[] { }, "HUD/VTB", "HUD Altimeter Selector Switch", "%0.1f"));    // elements["PTN_204"] = multiposition_switch_limited(_("HUD Altimeter Selector Switch"), devices.VTH_VTB, device_commands.Button_204, 204, 3, 0.5, true, 0)
            AddFunction(new Switch(this, VTH_VTB, "205", new SwitchPosition[] { }, "HUD/VTB", "Radar Altimeter Power Switch", "%0.1f"));    // elements["PTN_205"] = multiposition_switch_limited(_("Radar Altimeter Power Switch"), devices.VTH_VTB, device_commands.Button_205, 205, 3, 0.5, false, 0)
            AddFunction(new PushButton(this, VTH_VTB, "3206", "206", "HUD/VTB", "Auxiliary Gunsight"));    // elements["PTN_206"] = default_2_position_tumb(_("Auxiliary Gunsight"), devices.VTH_VTB, device_commands.Button_206, 206)
            AddFunction(new Axis(this, VTH_VTB, "3207", "207", 0.15d, 0d, 1d, "HUD/VTB", "Auxiliary Gunsight Deflection"));    // elements["PTN_207"] = default_axis_cycle(_("Auxiliary Gunsight Deflection"), devices.VTH_VTB, device_commands.Button_207, 207, 0, 0.01, true, 0)
            AddFunction(new PushButton(this, VTH_VTB, "3208", "208", "HUD/VTB", "A/G Gun Reticle Switch"));    // elements["PTN_208"] = default_2_position_tumb(_("A/G Gun Reticle Switch"), devices.VTH_VTB, device_commands.Button_208, 208)
            AddFunction(new Axis(this, VTH_VTB, "3209", "209", 0.15d, 0d, 1d, "HUD/VTB", "Target Wingspan Knob"));    // elements["PTN_209"] = default_axis_limited(_("Target Wingspan Knob"), devices.VTH_VTB, device_commands.Button_209, 209, 0, -0.03, true, 0, 99)
            AddFunction(new PushButton(this, VTH_VTB, "3210", "210", "HUD/VTB", "HUD Clear Button"));    // elements["PTN_210"] = default_button(_("HUD Clear Button"), devices.VTH_VTB, device_commands.Button_210, 210, 0, 1)
            AddFunction(new Axis(this, VTH_VTB, "3192", "192", 0.15d, 0d, 1d, "HUD/VTB", "Minimum Altitude Selector"));    // elements["PTN_192"] = default_axis(_("Minimum Altitude Selector"), devices.VTH_VTB, device_commands.Button_192, 192, 0, 0.2, true, 0)
            AddFunction(new PushButton(this, VTH_VTB, "3470", "470", "HUD/VTB", "Radar WOW Emitter Authorize Switch"));    // elements["PTN_470"] = default_2_position_tumb(_("Radar WOW Emitter Authorize Switch"), devices.VTH_VTB, device_commands.Button_470, 470)
            //AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3213", "213", "HUD/VTB", "Target Data Manual Entry Begin/End", "%0.1f"));    // elements["PTN_213"] = default_2_way_spring_switch(_("Target Data Manual Entry Begin/End"), devices.VTH_VTB, device_commands.Button_213, 213, true)
            //AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3214", "214", "HUD/VTB", "Bullseye Waypoint Selector", "%0.1f"));    // elements["PTN_214"] = default_2_way_spring_switch(_("Bullseye Waypoint Selector"), devices.VTH_VTB, device_commands.Button_214, 214, true)
            //AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3215", "215", "HUD/VTB", "Target Range from Bullseye", "%0.1f"));    // elements["PTN_215"] = default_2_way_spring_switch(_("Target Range from Bullseye"), devices.VTH_VTB, device_commands.Button_215, 215, true)
            //AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3216", "216", "HUD/VTB", "Target Bearing from Bullseye", "%0.1f"));    // elements["PTN_216"] = default_2_way_spring_switch(_("Target Bearing from Bullseye"), devices.VTH_VTB, device_commands.Button_216, 216, true)
            //AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3217", "217", "HUD/VTB", "Target Heading", "%0.1f"));    // elements["PTN_217"] = default_2_way_spring_switch(_("Target Heading"), devices.VTH_VTB, device_commands.Button_217, 217, true)
            //AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3218", "218", "HUD/VTB", "Target Altitude", "%0.1f"));    // elements["PTN_218"] = default_2_way_spring_switch(_("Target Altitude"), devices.VTH_VTB, device_commands.Button_218, 218, true)
            //AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3219", "219", "HUD/VTB", "Target Mach Number", "%0.1f"));    // elements["PTN_219"] = default_2_way_spring_switch(_("Target Mach Number"), devices.VTH_VTB, device_commands.Button_219, 219, true)
            //AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3220", "220", "HUD/VTB", "Target Age", "%0.1f"));    // elements["PTN_220"] = default_2_way_spring_switch(_("Target Age"), devices.VTH_VTB, device_commands.Button_220, 220, true)
            AddFunction(Switch.CreateThreeWaySwitch(this, VTH_VTB, "3213", "213", "1.0", "+", "0.0", "Neutral", "-1.0", "-", "HUD/VTB", "Target Data Manual Entry Begin/End", "%0.1f"));
            AddFunction(Switch.CreateThreeWaySwitch(this, VTH_VTB, "3214", "214", "1.0", "+", "0.0", "Neutral", "-1.0", "-", "HUD/VTB", "Bullseye Waypoint Selector", "%0.1f"));
            AddFunction(Switch.CreateThreeWaySwitch(this, VTH_VTB, "3215", "215", "1.0", "+", "0.0", "Neutral", "-1.0", "-", "HUD/VTB", "Target Range from Bullseye", "%0.1f"));
            AddFunction(Switch.CreateThreeWaySwitch(this, VTH_VTB, "3216", "216", "1.0", "+", "0.0", "Neutral", "-1.0", "-", "HUD/VTB", "Target Bearing from Bullseye", "%0.1f"));
            AddFunction(Switch.CreateThreeWaySwitch(this, VTH_VTB, "3217", "217", "1.0", "+", "0.0", "Neutral", "-1.0", "-", "HUD/VTB", "Target Heading", "%0.1f"));
            AddFunction(Switch.CreateThreeWaySwitch(this, VTH_VTB, "3218", "218", "1.0", "+", "0.0", "Neutral", "-1.0", "-", "HUD/VTB", "Target Altitude", "%0.1f"));
            AddFunction(Switch.CreateThreeWaySwitch(this, VTH_VTB, "3219", "219", "1.0", "+", "0.0", "Neutral", "-1.0", "-", "HUD/VTB", "Target Mach Number", "%0.1f"));
            AddFunction(Switch.CreateThreeWaySwitch(this, VTH_VTB, "3220", "220", "1.0", "+", "0.0", "Neutral", "-1.0", "-", "HUD/VTB", "Target Age", "%0.1f"));
            //AddFunction(new PushButton(this, VTH_VTB, "3221", "221", "HUD/VTB", "VTB Power Switch"));    // elements["PTN_221"] = default_2_position_tumb(_("VTB Power Switch"), devices.VTH_VTB, device_commands.Button_221, 221)
            AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3221", "221", "1.0", "ON",        "0.0", "OFF",     "HUD/VTB", "VTB Power Switch", "%0.1f"));    // elements["PTN_221"] = default_2_position_tumb(_("VTB Power Switch"), devices.VTH_VTB, device_commands.Button_221, 221)
            AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3222", "222", "1.0", "Declutter", "0.0", "Neutral", "HUD/VTB", "VTB Declutter",    "%0.1f"));    // elements["PTN_222"] = default_2_way_spring_switch(_("VTB Declutter"), devices.VTH_VTB, device_commands.Button_222, 222, true)
            AddFunction(Switch.CreateToggleSwitch(this, VTH_VTB, "3223", "223", "0.0", "AV",        "1.0", "AR",      "HUD/VTB", "VTB Orientation Selector (Inop)", "%0.1f"));    // elements["PTN_223"] = default_2_position_tumb(_("VTB Orientation Selector (Inop)"), devices.VTH_VTB, device_commands.Button_223, 223)
            //AddFunction(new PushButton(this, VTH_VTB, "3223", "223", "HUD/VTB", "VTB Orientation Selector (Inop)"));    // elements["PTN_223"] = default_2_position_tumb(_("VTB Orientation Selector (Inop)"), devices.VTH_VTB, device_commands.Button_223, 223)
            AddFunction(new Switch(this, VTH_VTB, "224", new SwitchPosition[] { }, "HUD/VTB", "Icons and Rulers Brightness", "%0.1f"));    // elements["PTN_224"] = multiposition_switch_limited(_("Icons and Rulers Brightness"), devices.VTH_VTB, device_commands.Button_224, 224, 8, 0.1, false, 0)
            AddFunction(new Switch(this, VTH_VTB, "225", new SwitchPosition[] { }, "HUD/VTB", "Video Brightness", "%0.1f"));    // elements["PTN_225"] = multiposition_switch_limited(_("Video Brightness"), devices.VTH_VTB, device_commands.Button_225, 225, 8, 0.1, false, 0)
            AddFunction(new Switch(this, VTH_VTB, "226", new SwitchPosition[] { }, "HUD/VTB", "Display Contrast", "%0.1f"));    // elements["PTN_226"] = multiposition_switch_limited(_("Display Contrast"), devices.VTH_VTB, device_commands.Button_226, 226, 8, 0.1, false, 0)
            AddFunction(new Switch(this, VTH_VTB, "227", new SwitchPosition[] { }, "HUD/VTB", "Display Brightness", "%0.1f"));    // elements["PTN_227"] = multiposition_switch_limited(_("Display Brightness"), devices.VTH_VTB, device_commands.Button_227, 227, 8, 0.1, false, 0)

            #endregion
            #region  AFCS
            AddFunction(new PushButton(this, AFCS, "3282", "282", "AFCS", "Autopilot Master Button"));    // elements["PTN_282"] = default_button(_("Autopilot Master Button"), devices.AFCS, device_commands.Button_282, 282, 0, 1)
            AddFunction(new PushButton(this, AFCS, "3285", "285", "AFCS", "Altitude Hold Button"));    // elements["PTN_285"] = default_button(_("Altitude Hold Button"), devices.AFCS, device_commands.Button_285, 285, 0, 1)
            AddFunction(new PushButton(this, AFCS, "3288", "288", "AFCS", "Selected Altitude Hold Button"));    // elements["PTN_288"] = default_button(_("Selected Altitude Hold Button"), devices.AFCS, device_commands.Button_288, 288, 0, 1)
            AddFunction(new PushButton(this, AFCS, "3294", "294", "AFCS", "Approach Hold Button"));    // elements["PTN_294"] = default_button(_("Approach Hold Button"), devices.AFCS, device_commands.Button_294, 294, 0, 1)
            AddFunction(new PushButton(this, AFCS, "3302", "302", "AFCS", "Autopilot Lights Test Button"));    // elements["PTN_302"] = default_button(_("Autopilot Lights Test Button"), devices.AFCS, device_commands.Button_302, 302, 0, 1)
            AddFunction(new Switch(this, AFCS, "299", new SwitchPosition[] { }, "AFCS", "Altitude 10 000 ft Selector", "%0.1f"));    // elements["PTN_299"] = default_multiposition_knob(_("Altitude 10,000 ft Selector"), devices.AFCS, device_commands.Button_299, 299,  6, 0.1, false, 0)
            AddFunction(new Switch(this, AFCS, "300", new SwitchPosition[] { }, "AFCS", "Altitude 1 000 ft Selector", "%0.1f"));    // elements["PTN_300"] = default_multiposition_knob(_("Altitude 1,000 ft Selector"),  devices.AFCS, device_commands.Button_300, 300, 10, 0.1, false, 0)
            AddFunction(new Switch(this, AFCS, "301", new SwitchPosition[] { }, "AFCS", "Altitude 100 ft Selector", "%0.1f"));    // elements["PTN_301"] = default_multiposition_knob(_("Altitude 100 ft Selector"),  devices.AFCS, device_commands.Button_301, 301, 10, 0.1, false4, 0)
                                                                                                       //AddFunction(Switch.CreateToggleSwitch(this, AFCS, "3508", "508", "AFCS", "Trim Control Mode Dial", "%0.1f"));    // elements["PTN_508"] = default_animated_lever(_("Trim Control Mode Dial"), devices.AFCS, device_commands.Button_508, 508,5.0)
                                                                                                       //AddFunction(Switch.CreateToggleSwitch(this, AFCS, "3509", "509", "AFCS", "Rudder Trim Paddle", "%0.1f"));    // elements["PTN_509"] = default_2_way_spring_switch(_("Rudder Trim Paddle"), devices.AFCS, device_commands.Button_509, 509,true)

            #endregion
            #region  FBW
            AddFunction(new PushButton(this, ENGINE, "3330", "330", "FBW", "FBW Spin Mode Switch"));    // elements["PTN_330"] = default_2_position_tumb(_("FBW Spin Mode Switch"), devices.ENGINE, device_commands.Button_330, 330)
//            AddFunction(new PushButton(this, ENGINE, "3420", "420", "FBW", "FBW Gain Mode Switch Cover"));    // elements["PTN_420"] = default_2_position_tumb(_("FBW Gain Mode Switch Cover"),  devices.ENGINE, device_commands.Button_420, 420)
            AddFunction(new PushButton(this, AFCS, "3423", "423", "FBW", "FBW Reset Button"));    // elements["PTN_423"] = default_button(_("FBW Reset Button"),  devices.AFCS, device_commands.Button_423, 423)

            #endregion
            #region  PELLES, SOURIES AND BECS
            AddFunction(new PushButton(this, ENGINE, "3460", "460", "PELLES, SOURIES AND BECS", "Intake Slats Operation Switch"));    // elements["PTN_460"] = default_2_position_tumb(_("Intake Slats Operation Switch"), devices.ENGINE, device_commands.Button_460, 460)
            AddFunction(new PushButton(this, ENGINE, "3461", "461", "PELLES, SOURIES AND BECS", "Intake Cones Operation Switch"));    // elements["PTN_461"] = default_2_position_tumb(_("Intake Cones Operation Switch"), devices.ENGINE, device_commands.Button_461, 461)
            AddFunction(new Switch(this, SUBSYSTEMS, "462", new SwitchPosition[] { }, "PELLES, SOURIES AND BECS", "Slats Operation Switch", "%0.1f"));    // elements["PTN_462"] = default_3_position_tumb(_("Slats Operation Switch"), devices.SUBSYSTEMS, device_commands.Button_462, 462, false, true)
            AddFunction(new Axis(this, SUBSYSTEMS, "3396", "396", 0.15d, 0d, 1d, "PELLES, SOURIES AND BECS", "Pedal Adjustment Lever"));    // elements["PTN_396"] = default_axis_limited(_("Pedal Adjustment Lever"),devices.SUBSYSTEMS,device_commands.Button_396,396, 0.5, -0.1, true, 0)
            AddFunction(new PushButton(this, SUBSYSTEMS, "3395", "395", "PELLES, SOURIES AND BECS", "Hydraulic System Selector"));    // elements["PTN_395"] = default_2_position_tumb(_("Hydraulic System Selector"), devices.SUBSYSTEMS, device_commands.Button_395, 395)
                                                                                                                                      // 
            #endregion
            #region  RADAR
            AddFunction(new Switch(this, RADAR, "481", new SwitchPosition[] { }, "RADAR", "Radar Illumination Switch", "%0.1f"));    // elements["PTN_481"] = multiposition_switch_limited(_("Radar Illumination Switch"), devices.RADAR, device_commands.Button_481, 481, 4, 0.5, false, -1)
            AddFunction(new PushButton(this, RADAR, "3482", "482", "RADAR", "Radar Test Button"));    // elements["PTN_482"] = default_button(_("Radar Test Button"), devices.RADAR, device_commands.Button_482, 482, 0, 1)
            AddFunction(new PushButton(this, RADAR, "3483", "483", "RADAR", "Radar Rearm Button"));    // elements["PTN_483"] = default_button(_("Radar Rearm Button"), devices.RADAR, device_commands.Button_483, 483, 0, 1)
            AddFunction(new PushButton(this, RADAR, "3484", "484", "RADAR", "Radar Doppler Reject Switch"));    // elements["PTN_484"] = default_2_position_tumb(_("Radar Doppler Reject Switch"), devices.RADAR, device_commands.Button_484, 484, 3, 0.5, false, 0)
            AddFunction(new Switch(this, RADAR, "485", new SwitchPosition[] { }, "RADAR", "Radar Contrast Switch", "%0.1f"));    // elements["PTN_485"] = multiposition_switch_limited(_("Radar Contrast Switch"), devices.RADAR, device_commands.Button_485, 485, 4, 0.5, false, -1)
            AddFunction(new Switch(this, RADAR, "486", new SwitchPosition[] { }, "RADAR", "Radar Power Selector", "%0.1f"));    // elements["PTN_486"] = multiposition_switch_limited(_("Radar Power Selector"), devices.RADAR, device_commands.Button_486, 486, 4, 0.33, false, 0)
            AddFunction(new Axis(this, RADAR, "3488", "488", 0.15d, 0d, 1d, "RADAR", "Radar Gain Dial"));    // elements["PTN_488"] = default_axis_limited(_("Radar Gain Dial"), devices.RADAR, device_commands.Button_488, 488, 10, 0.3, false, 0)
            AddFunction(new PushButton(this, RADAR, "3491", "491", "RADAR", "A/G Radar A Mode Switch"));    // elements["PTN_491"] = default_2_position_tumb(_("A/G Radar A Mode Switch"), devices.RADAR, device_commands.Button_491, 491, 0, 1)
            AddFunction(new PushButton(this, RADAR, "3493", "493", "RADAR", "A/G Radar DEC Mode Switch"));    // elements["PTN_493"] = default_2_position_tumb(_("A/G Radar DEC Mode Switch"), devices.RADAR, device_commands.Button_493, 493, 0, 1)
            AddFunction(new PushButton(this, RADAR, "Button_495", "495", "RADAR", "A/G Radar VISU Mode Switch"));    // elements["PTN_495"] = default_2_position_tumb(_("A/G Radar VISU Mode Switch"), devices.RADAR, device_commands.Button_495, 495, 0, 1)
            AddFunction(new PushButton(this, RADAR, "3499", "499", "RADAR", "Radar Grid Selector Switch"));    // elements["PTN_499"] = default_2_position_tumb(_("Radar Grid Selector Switch"), devices.RADAR, device_commands.Button_499, 499, 0, 1)
            AddFunction(new PushButton(this, RADAR, "3500", "500", "RADAR", "Target Memory Time Selector Switch"));    // elements["PTN_500"] = default_2_position_tumb(_("Target Memory Time Selector Switch"), devices.RADAR, device_commands.Button_500, 500, 0, 1)
            AddFunction(new Switch(this, RADAR, "502", new SwitchPosition[] { }, "RADAR", "Radar Scan Lines Selector", "%0.1f"));    // elements["PTN_502"] = multiposition_switch_limited(_("Radar Scan Lines Selector"), devices.RADAR, device_commands.Button_502, 502, 3, 0.5, true, 0)
            //AddFunction(Switch.CreateToggleSwitch(this, RADAR, "3503", "503", "RADAR", "Radar Range Selector Switch", "%0.1f"));    // elements["PTN_503"] = default_2_way_spring_switch(_("Radar Range Selector Switch"), devices.RADAR, device_commands.Button_503, 503,true)
            AddFunction(new PushButton(this, RADAR, "3504", "504", "RADAR", "A/A Radar STT Selector Button"));    // elements["PTN_504"] = default_button(_("A/A Radar STT Selector Button"), devices.RADAR, device_commands.Button_504, 504, 0, 1)
            AddFunction(new Switch(this, RADAR, "506", new SwitchPosition[] { }, "RADAR", "Radar Azimuth Selector", "%0.1f"));    // elements["PTN_506"] = multiposition_switch_limited(_("Radar Azimuth Selector"), devices.RADAR, device_commands.Button_506, 506, 3, 0.5, false, 0)
            AddFunction(new Switch(this, RADAR, "109", new SwitchPosition[] { }, "RADAR", "Radar PRF Switch", "%0.1f"));    // elements["PTN_109"] = default_3_position_tumb(_("Radar PRF Switch"), devices.RADAR, device_commands.Button_109, 109, false, true)
            AddFunction(new PushButton(this, RADAR, "3710", "710", "RADAR", "TDC Mode Switch"));    // elements["PTN_710"] = default_2_position_tumb(_("TDC Mode Switch"), devices.RADAR, device_commands.Button_710, 710, 0, 1)

            #endregion
            #region  RADAR IFF
            AddFunction(new Switch(this, RADAR, "598", new SwitchPosition[] { }, "RADAR IFF", "Radar IFF Mode Switch", "%0.1f"));    // elements["PTN_598"] = multiposition_switch_limited(_("Radar IFF Mode Switch"),   devices.RADAR, device_commands.Button_598, 598, 6, 0.2, false, 0)
            AddFunction(new PushButton(this, RADAR, "3599", "599", "RADAR IFF", "Radar IFF L/R Selector"));    // elements["PTN_599"] = default_2_position_tumb(_("Radar IFF L/R Selector"),       devices.RADAR, device_commands.Button_599, 599, 0, 1)
            AddFunction(new Switch(this, RADAR, "600", new SwitchPosition[] { }, "RADAR IFF", "Radar IFF Power Switch", "%0.1f"));    // elements["PTN_600"] = multiposition_switch_limited(_("Radar IFF Power Switch"),  devices.RADAR, device_commands.Button_600, 600, 3, 0.5, false, 0)
            AddFunction(new Switch(this, RADAR, "601", new SwitchPosition[] { }, "RADAR IFF", "Radar IFF Code-4 Selector", "%0.1f"));    // elements["PTN_601"] = default_multiposition_knob(_("Radar IFF Code-4 Selector"), devices.RADAR, device_commands.Button_601, 601, 10, 0.1, false, 0)
            AddFunction(new Switch(this, RADAR, "602", new SwitchPosition[] { }, "RADAR IFF", "Radar IFF Code-3 Selector", "%0.1f"));    // elements["PTN_602"] = default_multiposition_knob(_("Radar IFF Code-3 Selector"), devices.RADAR, device_commands.Button_602, 602, 10, 0.1, false, 0)
            AddFunction(new Switch(this, RADAR, "603", new SwitchPosition[] { }, "RADAR IFF", "Radar IFF Code-2 Selector", "%0.1f"));    // elements["PTN_603"] = default_multiposition_knob(_("Radar IFF Code-2 Selector"), devices.RADAR, device_commands.Button_603, 603, 10, 0.1, false, 0)
            AddFunction(new Switch(this, RADAR, "604", new SwitchPosition[] { }, "RADAR IFF", "Radar IFF Code-1 Selector", "%0.1f"));    // elements["PTN_604"] = default_multiposition_knob(_("Radar IFF Code-1 Selector"), devices.RADAR, device_commands.Button_604, 604, 10, 0.1, false, 0)
                                                                                                                                         // 
            #endregion
            #region  ELECTRICAL PANEL
            AddFunction(new PushButton(this, PWRPNL, "3654", "654", "ELECTRICAL PANEL", "Alert Network (QRA) Switch"));    // elements["PTN_654"] = default_2_position_tumb(_("Alert Network (QRA) Switch"),devices.PWRPNL, device_commands.Button_654, 654)

            #endregion
            #region  PSM
            AddFunction(new Switch(this, PCN_NAV, "665", new SwitchPosition[] { }, "PSM", "INS Auxiliary Heading/Horizon", "%0.1f"));    // elements["PTN_665"] = multiposition_switch_limited(_("INS Auxiliary Heading/Horizon"), devices.PCN_NAV, device_commands.Button_665, 665, 3, 0.5, false, 0)
                                                                                                                                         // 
            #endregion
            #region  EW PANEL
            AddFunction(new Axis(this, SYSLIGHTS, "3228", "228", 0.15d, 0d, 1d, "EW PANEL", "RWR Light Brightnes Control"));    // elements["PTN_228"] = default_axis_limited(_("RWR Light Brightnes Control"), devices.SYSLIGHTS, device_commands.Button_228, 228, 10, 0.1, false, 0)
            AddFunction(new Switch(this, RWR, "605", new SwitchPosition[] { }, "EW PANEL", "EW Mode Selector Switch", "%0.1f"));    // elements["PTN_605"] = default_3_position_tumb(_("EW Mode Selector Switch"), devices.RWR, device_commands.Button_605, 605, false, true)
            AddFunction(new Switch(this, RWR, "606", new SwitchPosition[] { }, "EW PANEL", "BR Power Switch", "%0.1f"));    // elements["PTN_606"] = multiposition_switch_limited(_("BR Power Switch"), devices.RWR, device_commands.Button_606, 606, 3, 0.5, false, 0)
            AddFunction(new Switch(this, RWR, "607", new SwitchPosition[] { }, "EW PANEL", "RWR Power Switch", "%0.1f"));    // elements["PTN_607"] = multiposition_switch_limited(_("RWR Power Switch"), devices.RWR, device_commands.Button_607, 607, 3, 0.5, false, 0)
            AddFunction(new Switch(this, DDM_IND, "608", new SwitchPosition[] { }, "EW PANEL", "D2M Power Switch", "%0.1f"));    // elements["PTN_608"] = multiposition_switch_limited(_("D2M Power Switch"), devices.DDM_IND, device_commands.Button_608, 608, 3, 0.5, false, 0)
            AddFunction(new Switch(this, RWR, "609", new SwitchPosition[] { }, "EW PANEL", "Decoy Release Mode Switch", "%0.1f"));    // elements["PTN_609"] = multiposition_switch_limited(_("Decoy Release Mode Switch"), devices.RWR, device_commands.Button_609, 609, 3, 0.5, false, 0)
            AddFunction(new Switch(this, RWR, "610", new SwitchPosition[] { }, "EW PANEL", "Decoy Release Program Knob", "%0.1f"));    // elements["PTN_610"] = multiposition_switch_limited(_("Decoy Release Program Knob"), devices.RWR, device_commands.Button_610, 610, 11, 0.1, false, 0)
            #endregion  
            #region  Panel Lights
            AddFunction(new PushButton(this, SYSLIGHTS, "3449", "449", "Panel Lights", "Police Lights Switch"));    // elements["PTN_449"] = default_2_position_tumb(_("Police Lights Switch"), devices.SYSLIGHTS, device_commands.Button_449, 449)
            AddFunction(new Switch(this, SYSLIGHTS, "450", new SwitchPosition[] { }, "Panel Lights", "Landing Lights Switch", "%0.1f"));    // elements["PTN_450"] = multiposition_switch_limited(_("Landing Lights Switch"), devices.SYSLIGHTS, device_commands.Button_450, 450, 3, 0.5, false, 0)
            AddFunction(new Switch(this, SYSLIGHTS, "452", new SwitchPosition[] { }, "Panel Lights", "SERPAM Recorder Switch", "%0.1f"));    // elements["PTN_452"] = multiposition_switch_limited(_("SERPAM Recorder Switch"), devices.SYSLIGHTS, device_commands.Button_452, 452, 3, 0.5, false, 0)
            AddFunction(new Switch(this, SYSLIGHTS, "453", new SwitchPosition[] { }, "Panel Lights", "Anti-Collision Lights Switch", "%0.1f"));    // elements["PTN_453"] = multiposition_switch_limited(_("Anti-Collision Lights Switch"), devices.SYSLIGHTS, device_commands.Button_453, 453, 3, 0.5, false, 0)
            AddFunction(new Switch(this, SYSLIGHTS, "454", new SwitchPosition[] { }, "Panel Lights", "Navigation Lights Switch", "%0.1f"));    // elements["PTN_454"] = multiposition_switch_limited(_("Navigation Lights Switch"), devices.SYSLIGHTS, device_commands.Button_454, 454, 3, 0.5, false, 0)
            AddFunction(new Switch(this, SYSLIGHTS, "455", new SwitchPosition[] { }, "Panel Lights", "Formation Lights Switch", "%0.1f"));    // elements["PTN_455"] = multiposition_switch_limited(_("Formation Lights Switch"), devices.SYSLIGHTS, device_commands.Button_455, 455, 3, 0.5, false, 0)
            AddFunction(new Axis(this, SYSLIGHTS, "3639", "639", 0.15d, 0d, 1d, "Panel Lights", "Dashboard U.V. Lights Knob"));    // elements["PTN_639"] = default_axis_limited(_("Dashboard U.V. Lights Knob"), devices.SYSLIGHTS, device_commands.Button_639, 639, 10, 0.3, false, 0)
            AddFunction(new Axis(this, SYSLIGHTS, "3640", "640", 0.15d, 0d, 1d, "Panel Lights", "Dashboard Panel Lights Knob"));    // elements["PTN_640"] = default_axis_limited(_("Dashboard Panel Lights Knob"), devices.SYSLIGHTS, device_commands.Button_640, 640, 10, 0.3, false, 0)
            AddFunction(new Axis(this, SYSLIGHTS, "3641", "641", 0.15d, 0d, 1d, "Panel Lights", "Red Flood Lights Knob"));    // elements["PTN_641"] = default_axis_limited(_("Red Flood Lights Knob"), devices.SYSLIGHTS, device_commands.Button_641, 641, 10, 0.3, false, 0)
            AddFunction(new Axis(this, SYSLIGHTS, "3642", "642", 0.15d, 0d, 1d, "Panel Lights", "Console Panel Lights Knob"));    // elements["PTN_642"] = default_axis_limited(_("Console Panel Lights Knob"), devices.SYSLIGHTS, device_commands.Button_642, 642, 10, 0.3, false, 0)
            AddFunction(new Axis(this, SYSLIGHTS, "3643", "643", 0.15d, 0d, 1d, "Panel Lights", "Casution/Advisory Lights Rheostat"));    // elements["PTN_643"] = default_axis_limited(_("Casution/Advisory Lights Rheostat"), devices.SYSLIGHTS, device_commands.Button_643, 643, 10, 0.3, false, 0)
            AddFunction(new Axis(this, SYSLIGHTS, "3644", "644", 0.15d, 0d, 1d, "Panel Lights", "White Flood Lights Knob"));    // elements["PTN_644"] = default_axis_limited(_("White Flood Lights Knob"), devices.SYSLIGHTS, device_commands.Button_644, 644, 10, 0.3, false, 0)
            AddFunction(new Axis(this, SYSLIGHTS, "Button_920", "920", 0.15d, 0d, 1d, "Panel Lights", "Refuel Lights Brightness Knob"));    // elements["PTN_920"] = default_axis_limited(_("Refuel Lights Brightness Knob"),devices.SYSLIGHTS,device_commands.Button_920, 920, 10, 0.3, false, 0)
            #endregion  
            #region  Fuel Panel 2
            AddFunction(new Switch(this, ENGPANEL, "193", new SwitchPosition[] { }, "Fuel Panel", "Refuel Transfer Switch", "%0.1f"));    // elements["PTN_193"] = multiposition_switch_limited(_("Refuel Transfer Switch"), devices.ENGPANEL, device_commands.Button_193, 193, 3, 0.5, false, 0)
            AddFunction(new Switch(this, INSTPANEL, "360", new SwitchPosition[] {
                new SwitchPosition("0.0", "0.0", "3360"),
                new SwitchPosition("0.1", "0.1", "3360"),
                new SwitchPosition("0.2", "0.2", "3360"),
                new SwitchPosition("0.3", "0.3", "3360")
            }, "Fuel Panel", "Bingo Fuel 1 000 kg Selector", "%0.1f"));    // elements["PTN_360"] = default_multiposition_knob(_("Bingo Fuel 1,000 kg Selector"), devices.INSTPANEL, device_commands.Button_360, 360,  10, 0.1, false, 0)
            AddFunction(new Switch(this, INSTPANEL, "361", new SwitchPosition[] {
                new SwitchPosition("0.0", "0.0", "3361"),
                new SwitchPosition("0.1", "0.1", "3361"),
                new SwitchPosition("0.2", "0.2", "3361"),
                new SwitchPosition("0.3", "0.3", "3361"),
                new SwitchPosition("0.4", "0.4", "3361"),
                new SwitchPosition("0.5", "0.5", "3361"),
                new SwitchPosition("0.6", "0.6", "3361"),
                new SwitchPosition("0.7", "0.7", "3361"),
                new SwitchPosition("0.8", "0.8", "3361"),
                new SwitchPosition("0.9", "0.9", "3361") }, "Fuel Panel", "Bingo Fuel 100 kg Selector", "%0.1f"));    // elements["PTN_361"] = default_multiposition_knob(_("Bingo Fuel 100 kg Selector"), devices.INSTPANEL, device_commands.Button_361, 361,  10, 0.1, false, 0)
            AddFunction(new Text(this, "2067", "Fuel Panel", "Fuel Burn Rate Display", "Three digit display showing Kg/Min Fuel"));

            #endregion
            #region  Radio Panel
            //            AddFunction(new PushButton(this, UHF, "3429", "429", "Radio Panel", "UHF Power 5W/25W Switch"));    // elements["PTN_429"] = default_2_position_tumb(_("UHF Power 5W/25W Switch"), devices.UHF, device_commands.Button_429, 429)
            //            AddFunction(new PushButton(this, UHF, "3430", "430", "Radio Panel", "UHF SIL Switch"));    // elements["PTN_430"] = default_2_position_tumb(_("UHF SIL Switch"), devices.UHF, device_commands.Button_430, 430)
            //AddFunction(Switch.CreateToggleSwitch(this, UHF, "3431", "431", "Radio Panel", "UHF E+A2 Switch", "%0.1f"));    // elements["PTN_431"] = default_2_way_spring_switch(_("UHF E+A2 Switch"), devices.UHF, device_commands.Button_431, 431, true)
            //            AddFunction(new PushButton(this, UHF, "3432", "432", "Radio Panel", "UHF CDE Switch"));    // elements["PTN_432"] = default_button(_("UHF CDE Switch"), devices.UHF, device_commands.Button_432, 432)
            //            AddFunction(new Switch(this, UHF, "433", new SwitchPosition[] { }, "Radio Panel", "UHF Mode Switch", "%0.1f"));    // elements["PTN_433"] = multiposition_switch_limited(_("UHF Mode Switch"), devices.UHF, device_commands.Button_433, 433, 4, 0.25, false, 0)
            //          AddFunction(new PushButton(this, UHF, "3434", "434", "Radio Panel", "UHF TEST Switch"));    // elements["PTN_434"] = default_button(_("UHF TEST Switch"), devices.UHF, device_commands.Button_434, 434)
            //            AddFunction(new Switch(this, UHF, "435", new SwitchPosition[] { }, "Radio Panel", "UHF Knob", "%0.1f"));    // elements["PTN_435"] = default_multiposition_knob(_("UHF Knob"), devices.UHF, device_commands.Button_435, 435, 20, 0.05,false,0.05)
            //            AddFunction(new PushButton(this, UVHF, "3437", "437", "Radio Panel", "U/VHF TEST Switch"));    // elements["PTN_437"] = default_button(_("U/VHF TEST Switch"), devices.UVHF, device_commands.Button_437, 437)
            //AddFunction(Switch.CreateToggleSwitch(this, UVHF, "3438", "438", "Radio Panel", "U/VHF E+A2 Switch", "%0.1f"));    // elements["PTN_438"] = default_2_way_spring_switch(_("U/VHF E+A2 Switch"), devices.UVHF, device_commands.Button_438, 438, true)
            //            AddFunction(new PushButton(this, UVHF, "3439", "439", "Radio Panel", "U/VHF SIL Switch"));    // elements["PTN_439"] = default_2_position_tumb(_("U/VHF SIL Switch"), devices.UVHF, device_commands.Button_439, 439)
            //            AddFunction(new Switch(this, UVHF, "440", new SwitchPosition[] { }, "Radio Panel", "U/VHF 100 MHz Selector", "%0.1f"));    // elements["PTN_440"] = default_multiposition_knob(_("U/VHF 100 MHz Selector"), devices.UVHF, device_commands.Button_440, 440, 4, 0.1, false, 0)
            //            AddFunction(new Switch(this, UVHF, "441", new SwitchPosition[] { }, "Radio Panel", "U/VHF 10 MHz Selector", "%0.1f"));    // elements["PTN_441"] = default_multiposition_knob(_("U/VHF 10 MHz Selector"), devices.UVHF, device_commands.Button_441, 441, 10, 0.1, false, 0)
            //            AddFunction(new Switch(this, UVHF, "442", new SwitchPosition[] { }, "Radio Panel", "U/VHF 1 MHz Selector", "%0.1f"));    // elements["PTN_442"] = default_multiposition_knob(_("U/VHF 1 MHz Selector"), devices.UVHF, device_commands.Button_442, 442, 10, 0.1, false, 0)
            //            AddFunction(new Switch(this, UVHF, "443", new SwitchPosition[] { }, "Radio Panel", "U/VHF 100 KHz Selector", "%0.1f"));    // elements["PTN_443"] = default_multiposition_knob(_("U/VHF 100 KHz Selector"), devices.UVHF, device_commands.Button_443, 443, 10, 0.1, false, 0)
            //            AddFunction(new Switch(this, UVHF, "444", new SwitchPosition[] { }, "Radio Panel", "U/VHF 25 KHz Selector", "%0.1f"));    // elements["PTN_444"] = default_multiposition_knob(_("U/VHF 25 KHz Selector"), devices.UVHF, device_commands.Button_444, 444, 4, 0.25, false, 0)
            //            AddFunction(new Switch(this, UVHF, "445", new SwitchPosition[] { }, "Radio Panel", "U/VHF Knob", "%0.1f"));    // elements["PTN_445"] = default_multiposition_knob(_("U/VHF Knob"), devices.UVHF, device_commands.Button_445, 445, 20, 0.05,false,0.05)
            //            AddFunction(new Switch(this, UVHF, "446", new SwitchPosition[] { }, "Radio Panel", "U/VHF Mode Switch 1", "%0.1f"));    // elements["PTN_446"] = multiposition_switch_limited(_("U/VHF Mode Switch 1"), devices.UVHF, device_commands.Button_446, 446, 5, 0.25, false, 0)
            //            AddFunction(new PushButton(this, UVHF, "3447", "447", "Radio Panel", "U/VHF Power 5W/25W Switch"));    // elements["PTN_447"] = default_2_position_tumb(_("U/VHF Power 5W/25W Switch"), devices.UVHF, device_commands.Button_447, 447)
            //            AddFunction(new Switch(this, UVHF, "448", new SwitchPosition[] { }, "Radio Panel", "U/VHF Manual/Preset Mode Selector", "%0.1f"));    // elements["PTN_448"] = multiposition_switch_limited(_("U/VHF Manual/Preset Mode Selector"), devices.UVHF, device_commands.Button_448, 448, 3, 0.50, false, 0)
            #endregion
            #region  Miscellaneous Left Panel
            AddFunction(new PushButton(this, MISCPANELS, "3400", "400", "Miscellaneous Left Panel", "Cockpit Clock"));    // elements["PTN_400"] = default_2_position_tumb(_("Cockpit Clock"), devices.MISCPANELS, device_commands.Button_400, 400)
            AddFunction(new PushButton(this, MISCPANELS, "3458", "458", "Miscellaneous Left Panel", "Anti-Skid Switch Cover"));    // elements["PTN_458"] = default_2_position_tumb(_("Anti-Skid Switch Cover"), devices.MISCPANELS, device_commands.Button_458, 458)
            AddFunction(new PushButton(this, MISCPANELS, "3459", "459", "Miscellaneous Left Panel", "Anti-Skid Switch"));    // elements["PTN_459"] = default_2_position_tumb(_("Anti-Skid Switch"), devices.MISCPANELS, device_commands.Button_459, 459)
            //AddFunction(Switch.CreateToggleSwitch(this, MISCPANELS, "3666", "666", "Miscellaneous Left Panel", "Parking Brake Lever", "%0.1f"));    // elements["PTN_666"] = default_animated_lever(_("Parking Brake Lever"), devices.MISCPANELS, device_commands.Button_666, 666,5.0)
            //AddFunction(Switch.CreateToggleSwitch(this, SUBSYSTEMS, "3456", "456", "Miscellaneous Left Panel", "Canopy Jettison", "%0.1f"));    // elements["PTN_456"] = default_animated_lever(_("Canopy Jettison"),devices.SUBSYSTEMS, device_commands.Button_456, 456,5.0)
            //AddFunction(Switch.CreateToggleSwitch(this, MISCPANELS, "3457", "457", "Miscellaneous Left Panel", "Drag Chute Lever", "%0.1f"));    // elements["PTN_457"] = default_animated_lever(_("Drag Chute Lever"), devices.MISCPANELS, device_commands.Button_457, 457,5.0)
            //AddFunction(Switch.CreateToggleSwitch(this, MISCPANELS, "3807", "807", "Miscellaneous Left Panel", "Nose Wheel Steering / IFF Interrogation Button", "%0.1f"));    // elements["PTN_807"] = default_2_way_spring_switch(_("Nose Wheel Steering / IFF Interrogation Button"), devices.MISCPANELS, device_commands.Button_807, 807)
            AddFunction(new PushButton(this, SUBSYSTEMS, "3655", "655", "Miscellaneous Left Panel", "Canopy Rest"));    // elements["PTN_655"] = default_2_position_tumb(_("Canopy Rest"),devices.SUBSYSTEMS, device_commands.Button_655, 655)
            //AddFunction((this, SUBSYSTEMS, "3656", "656", "Miscellaneous Left Panel", "Canopy Lock/Neutral/Lower Lever"));    // elements["PTN_656"] = default_multiposition_animated_lever(_("Canopy Lock/Neutral/Lower Lever"), devices.SUBSYSTEMS, device_commands.Button_656, 656, 3, 0.5, false, 0,2.5)
            AddFunction(new PushButton(this, PCN_NAV, "3905", "905", "Miscellaneous Left Panel", "Emergency Compass"));    // elements["PTN_905"] = default_2_position_tumb(_("Emergency Compass"),devices.PCN_NAV, device_commands.Button_905, 905)
            AddFunction(new PushButton(this, SUBSYSTEMS, "3907", "907", "Miscellaneous Left Panel", "Canopy Handle"));    // elements["PTN_907"] = default_2_position_tumb(_("Canopy Handle"),devices.SUBSYSTEMS, device_commands.Button_907, 907)
            AddFunction(new PushButton(this, SUBSYSTEMS, "3908", "908", "Miscellaneous Left Panel", "Canopy Handle (alt?)"));    // elements["PTN_908"] = default_2_position_tumb(_("Canopy Handle"),devices.SUBSYSTEMS, device_commands.Button_908, 908)
            AddFunction(new PushButton(this, MISCPANELS, "3909", "909", "Miscellaneous Left Panel", "Mirror Rendering Toggle"));    // elements["PTN_909"] = default_2_position_tumb(_("Mirror Rendering Toggle"), devices.MISCPANELS, device_commands.Button_909, 909)
            #endregion  
            #region  Miscellaneous Right Panel
            AddFunction(new Switch(this, ENGINE, "657", new SwitchPosition[] { }, "Miscellaneous Right Panel", "Emergency Hydraulic Pump Switch", "%0.1f"));    // elements["PTN_657"] = multiposition_switch_spring(_("Emergency Hydraulic Pump Switch"), devices.ENGINE, device_commands.Button_657, device_commands.Button_657, 657)
            AddFunction(new PushButton(this, SYSLIGHTS, "3658", "658", "Miscellaneous Right Panel", "Audio Warning Switch"));    // elements["PTN_658"] = default_2_position_tumb(_("Audio Warning Switch"), devices.SYSLIGHTS, device_commands.Button_658, 658)
            AddFunction(new PushButton(this, MISCPANELS, "3659", "659", "Miscellaneous Right Panel", "Pitot Heat Cover"));    // elements["PTN_659"] = default_2_position_tumb(_("Pitot Heat Cover"), devices.MISCPANELS, device_commands.Button_659, 659)
            AddFunction(new PushButton(this, MISCPANELS, "3660", "660", "Miscellaneous Right Panel", "Pitot Heat Switch"));    // elements["PTN_660"] = default_2_position_tumb(_("Pitot Heat Switch"), devices.MISCPANELS, device_commands.Button_660, 660)
            #endregion  
            #region  Miscellaneous Seat
            //AddFunction(Switch.CreateToggleSwitch(this, MISCPANELS, "3900", "900", "Miscellaneous Seat", "Seat Adjustment Switch", "%0.1f"));    // elements["PTN_900"] = default_2_way_spring_switch(_("Seat Adjustment Switch"), devices.MISCPANELS, device_commands.Button_900, 900,true)
            AddFunction(new PushButton(this, ECS, "3910", "910", "Miscellaneous Seat", "LOX Dilution Lever"));    // elements["PTN_910"] = default_2_position_tumb(_("LOX Dilution Lever"),devices.ECS, device_commands.Button_910,  910)
            AddFunction(new PushButton(this, ECS, "3912", "912", "Miscellaneous Seat", "LOX Emergency Supply"));    // elements["PTN_912"] = default_2_position_tumb(_("LOX Emergency Supply"), devices.ECS, device_commands.Button_912, 912)
            #endregion  
            #region  Sound Panel
            AddFunction(new PushButton(this, SYSLIGHTS, "3700", "700", "Sound Panel", "AMPLIS Selector Knob"));    // elements["PTN_700"] = default_2_position_tumb(_("AMPLIS Selector Knob"), devices.SYSLIGHTS, device_commands.Button_700, 700)
            AddFunction(new Axis(this, SYSLIGHTS, "3701", "701", 0.15d, 0d, 1d, "Sound Panel", "VOR/ILS Volume Knob"));    // elements["PTN_701"] = default_axis_limited(_("VOR/ILS Volume Knob"), devices.SYSLIGHTS, device_commands.Button_701, 701, 0.8, 0.5, true, false, {0.0, 1.0})
            AddFunction(new Axis(this, SYSLIGHTS, "3702", "702", 0.15d, 0d, 1d, "Sound Panel", "TACAN Volume Knob"));    // elements["PTN_702"] = default_axis_limited(_("TACAN Volume Knob"), devices.SYSLIGHTS, device_commands.Button_702, 702, 0.8, 0.5, true, false, {0.0, 1.0})
            AddFunction(new Axis(this, SYSLIGHTS, "3703", "703", 0.15d, 0d, 1d, "Sound Panel", "MAGIC Tone Volume Knob"));    // elements["PTN_703"] = default_axis_limited(_("MAGIC Tone Volume Knob"), devices.SYSLIGHTS, device_commands.Button_703, 703, 0.8, 0.5, true, false, {0.0, 1.0})
            AddFunction(new Axis(this, SYSLIGHTS, "3704", "704", 0.15d, 0d, 1d, "Sound Panel", "TB APP Volume Knob"));    // elements["PTN_704"] = default_axis_limited(_("TB APP Volume Knob"), devices.SYSLIGHTS, device_commands.Button_704, 704, 0.8, 0.5, true, false, {0.0, 1.0})
            AddFunction(new Axis(this, SYSLIGHTS, "3705", "705", 0.15d, 0d, 1d, "Sound Panel", "Marker Signal Volume Knob"));    // elements["PTN_705"] = default_axis_limited(_("Marker Signal Volume Knob"), devices.SYSLIGHTS, device_commands.Button_705, 705, 0.8, 0.5, true, false, {0.0, 1.0})
            AddFunction(new Axis(this, SYSLIGHTS, "3706", "706", 0.15d, 0d, 1d, "Sound Panel", "UHF Radio Volume Knob"));    // elements["PTN_706"] = default_axis_limited(_("UHF Radio Volume Knob"), devices.SYSLIGHTS, device_commands.Button_706, 706, 0.8, 0.5, true, false, {0.0, 1.0})
            AddFunction(new Axis(this, SYSLIGHTS, "3707", "707", 0.15d, 0d, 1d, "Sound Panel", "V/UHF Radio Volume Knob"));    // elements["PTN_707"] = default_axis_limited(_("V/UHF Radio Volume Knob"), devices.SYSLIGHTS, device_commands.Button_707, 707, 0.8, 0.5, true, false, {0.0, 1.0})
            #endregion  
            #region  Flight Instruments
            AddFunction(new Axis(this, FLIGHTINST, "3309", "309", 0.15d, 0d, 1d, "Flight Instruments", "Barometric Pressure Calibration"));    // elements["PTN_309"] = default_axis(_("Barometric Pressure Calibration"),devices.FLIGHTINST,device_commands.Button_309,309)
            AddFunction(new PushButton(this, FLIGHTINST, "3314", "314", "Flight Instruments", "ADI Cage Lever"));    // elements["PTN_314"] = default_2_position_tumb(_("ADI Cage Lever"),devices.FLIGHTINST, device_commands.Button_314, 314)
            AddFunction(new PushButton(this, FLIGHTINST, "3315", "315", "Flight Instruments", "ADI Backlight Switch"));    // elements["PTN_315"] = default_2_position_tumb(_("ADI Backlight Switch"),devices.FLIGHTINST,device_commands.Button_315,315)
            #endregion  
            #region  ECS Panel
            AddFunction(new PushButton(this, ECS, "3630", "630", "ECS Panel", "ECS Main Mode Switch"));    // elements["PTN_630"] = default_2_position_tumb(_("ECS Main Mode Switch"),devices.ECS, device_commands.Button_630, 630)
            AddFunction(new PushButton(this, ECS, "3631", "631", "ECS Panel", "ECS C Button"));    // elements["PTN_631"] = default_button(_("ECS C Button"), devices.ECS, device_commands.Button_631, 631, 1, 1)
            AddFunction(new PushButton(this, ECS, "3633", "633", "ECS Panel", "ECS F Button"));    // elements["PTN_633"] = default_button(_("ECS F Button"), devices.ECS, device_commands.Button_633, 633, 1, 1)
            AddFunction(new PushButton(this, ECS, "3635", "635", "ECS Panel", "ECS Cond Switch"));    // elements["PTN_635"] = default_2_position_tumb(_("ECS Cond Switch"),devices.ECS, device_commands.Button_635, 635)
            AddFunction(new PushButton(this, ECS, "3636", "636", "ECS Panel", "ECS Air Exchange Switch"));    // elements["PTN_636"] = default_2_position_tumb(_("ECS Air Exchange Switch"),devices.ECS, device_commands.Button_636, 636)
            AddFunction(new Axis(this, ECS, "3637", "637", 0.15d, 0d, 1d, "ECS Panel", "ECS Temperature Select Knob"));    // elements["PTN_637"] = default_axis_limited_cycle(_("ECS Temperature Select Knob"), devices.ECS, device_commands.Button_637, 637, 0.8, 0.5, true, false, {-1.0, 1.0})
            AddFunction(new PushButton(this, ECS, "3638", "638", "ECS Panel", "ECS Defog Switch"));    // elements["PTN_638"] = default_2_position_tumb(_("ECS Defog Switch"),devices.ECS, device_commands.Button_638, 638)
                                                                                                       // 
            #endregion
            #region  IFF
            AddFunction(new Switch(this, INSTPANEL, "377", new SwitchPosition[] { }, "IFF", "Mode-1 Tens Selector", "%0.1f"));
            AddFunction(new Switch(this, INSTPANEL, "378", new SwitchPosition[] { }, "IFF", "Mode-1 Ones Selector", "%0.1f"));
            AddFunction(new Switch(this, INSTPANEL, "379", new SwitchPosition[] { }, "IFF", "Mode-3A Thousands Selector", "%0.1f"));
            AddFunction(new Switch(this, INSTPANEL, "380", new SwitchPosition[] { }, "IFF", "Mode-3A Hundreds Selector", "%0.1f"));
            AddFunction(new Switch(this, INSTPANEL, "381", new SwitchPosition[] { }, "IFF", "Mode-3A Tens Selector", "%0.1f"));
            AddFunction(new Switch(this, INSTPANEL, "382", new SwitchPosition[] { }, "IFF", "Mode-3A Ones Selector", "%0.1f"));
            AddFunction(new Switch(this, INSTPANEL, "383", new SwitchPosition[] { }, "IFF", "Ident Power Switch", "%0.1f"));
            AddFunction(new PushButton(this, INSTPANEL, "3384", "384", "IFF", "Mode-1 Switch"));
            AddFunction(new PushButton(this, INSTPANEL, "3385", "385", "IFF", "Mode-2 Switch"));
            AddFunction(new PushButton(this, INSTPANEL, "3386", "386", "IFF", "Mode-3A Switch"));
            AddFunction(new PushButton(this, INSTPANEL, "3387", "387", "IFF", "Mode-C Switch"));
            #endregion

            #region EVF (TAF) Panel
            AddFunction(new PushButton(this, TAF, EVF_Commands.Button_1006.ToString("d"), "1006", "EVF (TAF) Panel", "Test"));
            AddFunction(new RotaryEncoder(this, TAF, EVF_Commands.Button_1004.ToString("d"), "1004", 0.05d, "EVF (TAF) Panel", "Channel Selector"));
            AddFunction(new Text(this, "2082", "EVF (TAF) Panel", "EVF Display", "Two digit display on the EVF Panel"));

            #endregion
        }
    }
}
 