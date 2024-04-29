using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RDCReader
{
    public static class RDCDefinition
    {
        public const int SEC_ROM_RD_DOMAIN_VER1_KEY_LENGTH = 256;
        public const int SEC_ROM_OEM2_VER1_KEY_LENGTH = 256;
        public const int SEC_ROM_RD_SW_VER1_KEY_LENGTH = 256;
        public const int SEC_ROM_RD_PROT_APP_VER1_KEY_LENGTH = 256;

        public const int SEC_ROM_PUBLIC_ID_LENGTH = 20;
        public const int SEC_ROM_PK_HEADER_SIZE = 8;

        public const int SEC_ROM_RD_DOMAIN_VER1_KEY_TOTAL_LENGTH = 
            (SEC_ROM_RD_DOMAIN_VER1_KEY_LENGTH + SEC_ROM_PK_HEADER_SIZE);

        public const int SEC_ROM_RD_SW_VER1_KEY_TOTAL_LENGTH =
            (SEC_ROM_RD_SW_VER1_KEY_LENGTH + SEC_ROM_PK_HEADER_SIZE);

        public const int SEC_ROM_RD_PROT_APP_VER1_KEY_TOTAL_LENGTH =
            (SEC_ROM_RD_PROT_APP_VER1_KEY_LENGTH + SEC_ROM_PK_HEADER_SIZE);

        public const uint TZBSP_SHARED_IMEM_NOKIARDC = (0xFE87F000 + 0xA94);
        public const int RDC_CRASH_MAGIC = 0x68;

        public const int RDC_PRESENT = 1;
        public const int RDC_MISSING = 0;
        public const int RDC_NOT_INITIALIZED = 0xFF;

        public struct SIGNER_INFO
        {
            uint server_id;
            uint time_stamp;
            uint serial_number;
            uint user_id;
        }

        public struct KEY_TYPE_STR
        {
            byte version;
            byte type;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SEC_ROM_RD_DOMAIN_CERTIFICATE_VER1
        {
            uint magic;
            uint key_index;    /* Shall always be SEC_ROM_OEM2_KEY_INDEX */
            uint debug_control_mask;
            SIGNER_INFO signer;
            KEY_TYPE_STR domain_key_type;           /* allocates two bytes */
            KEY_TYPE_STR signing_key_type;          /* allocates two bytes */
            uint sw_debug_control_mask;
            uint dss_signing_info;                 /* Reserved for DSS usage */
            uint security_mode_mask;
            byte version;
            byte sign_algo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            byte[] spare2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            uint[] spare1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SEC_ROM_RD_DOMAIN_VER1_KEY_TOTAL_LENGTH)]
            byte[] rd_domain_key;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SEC_ROM_OEM2_VER1_KEY_LENGTH)]
            byte[] rd_domain_signature;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SEC_ROM_RD_CERTIFICATE_VER1
        {
            uint magic;
            uint key_index;    /* Shall always be SEC_ROM_RD_DOMAIN_KEY_INDEX */
            SEC_ROM_RD_DOMAIN_CERTIFICATE_VER1 domain_cert;
            uint debug_control;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SEC_ROM_PUBLIC_ID_LENGTH)]
            byte[] public_id;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SEC_ROM_RD_SW_VER1_KEY_TOTAL_LENGTH)]
            byte[] rd_sw_key;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SEC_ROM_RD_PROT_APP_VER1_KEY_TOTAL_LENGTH)]
            byte[] rd_prot_app_key;
            SIGNER_INFO signer;
            KEY_TYPE_STR rd_sw_key_type;            /* allocates two bytes */
            KEY_TYPE_STR rd_prot_app_key_type;      /* allocates two bytes */
            KEY_TYPE_STR signing_key_type;          /* allocates two bytes */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            byte[] spare0;
            uint sw_debug_control;
            uint security_mode_control;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            uint[] spare1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SEC_ROM_RD_DOMAIN_VER1_KEY_LENGTH)]
            byte[] header_signature;
        }
    }
}
