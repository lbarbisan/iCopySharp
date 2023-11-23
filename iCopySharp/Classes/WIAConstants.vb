﻿'iCopy - Simple Photocopier
'Copyright (C) 2007-2018 Matteo Rossi

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.

'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <http://www.gnu.org/licenses/>.

Public Enum WIA_ERRORS
    WIA_ERROR_UNSPECIFIED_ERROR = &H80210000
    WIA_ERROR_GENERAL_ERROR = &H80210001
    WIA_ERROR_PAPER_JAM = &H80210002
    WIA_ERROR_PAPER_EMPTY = &H80210003
    WIA_ERROR_PAPER_PROBLEM = &H80210004
    WIA_ERROR_OFFLINE = &H80210005
    WIA_ERROR_BUSY = &H80210006
    WIA_ERROR_WARMING_UP = &H80210007
    WIA_ERROR_USER_INTERVENTION = &H80210008
    WIA_ERROR_ITEM_DELETED = &H80210009
    WIA_ERROR_DEVICE_COMMUNICATION = &H8021000A
    WIA_ERROR_INVALID_COMMAND = &H8021000B
    WIA_ERROR_INCORRECT_HARDWARE_SETTING = &H8021000C
    WIA_ERROR_DEVICE_LOCKED = &H8021000D
    WIA_ERROR_EXCEPTION_IN_DRIVER = &H8021000E
    WIA_ERROR_INVALID_DRIVER_RESPONSE = &H8021000F
    WIA_ERROR_NOT_REGISTERED = -2147221164
    WIA_ERROR_NO_SCANNER_CONNECTED = &H80210015
    WIA_ERROR_NO_SCANNER_SELECTED = &H80210064
    WIA_ERROR_CONNECTION_ERROR = &H80070077
    WIA_ERROR_UNKNOWN_ERROR = &H80210067

    'Found by myself
    WIA_ERROR_SCANNER_NOT_FOUND = &H8021006B
    WIA_ERROR_PROPERTY_DONT_EXIST = &H8021006A
End Enum

Enum WIA_PROPERTIES
    WIA_RESERVED_FOR_NEW_PROPS = 1024
    WIA_DIP_FIRST = 2
    WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS
    WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS

    ' Scanner only device properties (DPS)
    WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS
    WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13
    WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14
End Enum

<Flags()> _
Enum WIA_DPS_DOCUMENT_HANDLING_CAPABILITIES
    FEED = &H1                      '1
    FLAT = &H2                      '2
    DUP = &H4                       '4
    DETECT_FLAT = &H8               '8
    DETECT_SCAN = &H10              '16
    DETECT_FEED = &H20              '32
    DETECT_DUP = &H40               '64
    DETECT_FEED_AVAIL = &H80        '128
    DETECT_DUP_AVAIL = &H100        '256
    'Vista and 7 only               '512
    FILM_TPA = &H200                '1024
    DETECT_FILM_TPA = &H400         '2048
    STOR = &H800
    DETECT_STOR = &H1000
    ADVANCED_DUP = &H2000
    AUTO_SOURCE = &H8000
End Enum

<Flags()> _
Enum WIA_DPS_DOCUMENT_HANDLING_STATUS
    FEED_READY = &H1        '1
    FLAT_READY = &H2        '2
    DUP_READY = &H4         '4
    FLAT_COVER_UP = &H8     '8
    PATH_COVER_UP = &H10    '16
    PAPER_JAM = &H20        '32

    'Only for Vista and 7   
    FILM_TPA_READY = &H40       '64
    STORAGE_READY = &H80        '128
    STORAGE_FULL = &H100        '256
    MULTIPLE_FEED = &H200       '512
    DEVICE_ATTENTION = &H400    '1024
    LAMP_ERR = &H800            '2048
End Enum

<Flags()> _
Enum WIA_DPS_DOCUMENT_HANDLING_SELECT
    FEEDER = &H1            '1
    FLATBED = &H2           '2
    DUPLEX = &H4            '4
    FRONT_FIRST = &H8       '8
    BACK_FIRST = &H10       '16
    FRONT_ONLY = &H20       '32
    BACK_ONLY = &H40        '64
    NEXT_PAGE = &H80        '128
    PREFEED = &H100         '256
    AUTO_ADVANCE = &H200    '512
End Enum

Enum WIA_IPS_PAGE_SIZE
    '                                           size (in 1/1000 of an inch)
    WIA_PAGE_A4 = 0                          '  8267 x 11692 
    WIA_PAGE_LETTER = 1                      '  8500 x 11000
    WIA_PAGE_CUSTOM = 2                      ' (current extent settings)
    WIA_PAGE_USLEGAL = 3                     '  8500 x 14000
    WIA_PAGE_USLETTER = WIA_PAGE_LETTER
    WIA_PAGE_USLEDGER = 4                    '  11000 x 17000
    WIA_PAGE_USSTATEMENT = 5                 '  5500 x  8500
    WIA_PAGE_BUSINESSCARD = 6                '  3543 x  2165
End Enum