using System;

namespace iCopy
{
    // iCopy - Simple Photocopier
    // Copyright (C) 2007-2018 Matteo Rossi

    // This program is free software: you can redistribute it and/or modify
    // it under the terms of the GNU General Public License as published by
    // the Free Software Foundation, either version 3 of the License, or
    // (at your option) any later version.

    // This program is distributed in the hope that it will be useful,
    // but WITHOUT ANY WARRANTY; without even the implied warranty of
    // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    // GNU General Public License for more details.

    // You should have received a copy of the GNU General Public License
    // along with this program.  If not, see <http://www.gnu.org/licenses/>.

    public enum WIA_ERRORS
    {
        WIA_ERROR_UNSPECIFIED_ERROR = int.MinValue + 0x00210000,
        WIA_ERROR_GENERAL_ERROR = int.MinValue + 0x00210001,
        WIA_ERROR_PAPER_JAM = int.MinValue + 0x00210002,
        WIA_ERROR_PAPER_EMPTY = int.MinValue + 0x00210003,
        WIA_ERROR_PAPER_PROBLEM = int.MinValue + 0x00210004,
        WIA_ERROR_OFFLINE = int.MinValue + 0x00210005,
        WIA_ERROR_BUSY = int.MinValue + 0x00210006,
        WIA_ERROR_WARMING_UP = int.MinValue + 0x00210007,
        WIA_ERROR_USER_INTERVENTION = int.MinValue + 0x00210008,
        WIA_ERROR_ITEM_DELETED = int.MinValue + 0x00210009,
        WIA_ERROR_DEVICE_COMMUNICATION = int.MinValue + 0x0021000A,
        WIA_ERROR_INVALID_COMMAND = int.MinValue + 0x0021000B,
        WIA_ERROR_INCORRECT_HARDWARE_SETTING = int.MinValue + 0x0021000C,
        WIA_ERROR_DEVICE_LOCKED = int.MinValue + 0x0021000D,
        WIA_ERROR_EXCEPTION_IN_DRIVER = int.MinValue + 0x0021000E,
        WIA_ERROR_INVALID_DRIVER_RESPONSE = int.MinValue + 0x0021000F,
        WIA_ERROR_NOT_REGISTERED = -2147221164,
        WIA_ERROR_NO_SCANNER_CONNECTED = int.MinValue + 0x00210015,
        WIA_ERROR_NO_SCANNER_SELECTED = int.MinValue + 0x00210064,
        WIA_ERROR_CONNECTION_ERROR = int.MinValue + 0x00070077,
        WIA_ERROR_UNKNOWN_ERROR = int.MinValue + 0x00210067,

        // Found by myself
        WIA_ERROR_SCANNER_NOT_FOUND = int.MinValue + 0x0021006B,
        WIA_ERROR_PROPERTY_DONT_EXIST = int.MinValue + 0x0021006A
    }

    enum WIA_PROPERTIES
    {
        WIA_RESERVED_FOR_NEW_PROPS = 1024,
        WIA_DIP_FIRST = 2,
        WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS,
        WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS,

        // Scanner only device properties (DPS)
        WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS,
        WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13,
        WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14
    }

    [Flags()]
    enum WIA_DPS_DOCUMENT_HANDLING_CAPABILITIES
    {
        FEED = 0x1,                      // 1
        FLAT = 0x2,                      // 2
        DUP = 0x4,                       // 4
        DETECT_FLAT = 0x8,               // 8
        DETECT_SCAN = 0x10,              // 16
        DETECT_FEED = 0x20,              // 32
        DETECT_DUP = 0x40,               // 64
        DETECT_FEED_AVAIL = 0x80,        // 128
        DETECT_DUP_AVAIL = 0x100,        // 256
                                         // Vista and 7 only               '512
        FILM_TPA = 0x200,                // 1024
        DETECT_FILM_TPA = 0x400,         // 2048
        STOR = 0x800,
        DETECT_STOR = 0x1000,
        ADVANCED_DUP = 0x2000,
        AUTO_SOURCE = 0x8000
    }

    [Flags()]
    enum WIA_DPS_DOCUMENT_HANDLING_STATUS
    {
        FEED_READY = 0x1,        // 1
        FLAT_READY = 0x2,        // 2
        DUP_READY = 0x4,         // 4
        FLAT_COVER_UP = 0x8,     // 8
        PATH_COVER_UP = 0x10,    // 16
        PAPER_JAM = 0x20,        // 32

        // Only for Vista and 7   
        FILM_TPA_READY = 0x40,       // 64
        STORAGE_READY = 0x80,        // 128
        STORAGE_FULL = 0x100,        // 256
        MULTIPLE_FEED = 0x200,       // 512
        DEVICE_ATTENTION = 0x400,    // 1024
        LAMP_ERR = 0x800            // 2048
    }

    [Flags()]
    enum WIA_DPS_DOCUMENT_HANDLING_SELECT
    {
        FEEDER = 0x1,            // 1
        FLATBED = 0x2,           // 2
        DUPLEX = 0x4,            // 4
        FRONT_FIRST = 0x8,       // 8
        BACK_FIRST = 0x10,       // 16
        FRONT_ONLY = 0x20,       // 32
        BACK_ONLY = 0x40,        // 64
        NEXT_PAGE = 0x80,        // 128
        PREFEED = 0x100,         // 256
        AUTO_ADVANCE = 0x200    // 512
    }

    enum WIA_IPS_PAGE_SIZE
    {
        // size (in 1/1000 of an inch)
        WIA_PAGE_A4 = 0,                          // 8267 x 11692 
        WIA_PAGE_LETTER = 1,                      // 8500 x 11000
        WIA_PAGE_CUSTOM = 2,                      // (current extent settings)
        WIA_PAGE_USLEGAL = 3,                     // 8500 x 14000
        WIA_PAGE_USLETTER = WIA_PAGE_LETTER,
        WIA_PAGE_USLEDGER = 4,                    // 11000 x 17000
        WIA_PAGE_USSTATEMENT = 5,                 // 5500 x  8500
        WIA_PAGE_BUSINESSCARD = 6                // 3543 x  2165
    }
}