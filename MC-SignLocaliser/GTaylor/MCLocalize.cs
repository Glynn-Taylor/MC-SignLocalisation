using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCGT_SignTranslator.GTaylor
{
    public class MCLocalize
    {
        public Dictionary<string, string> LocalNameToName = new Dictionary<string, string>
        {
            {@"frikaans",@"Afrikaans"},{@"اللغة العربية",@"Arabic"},{@"Asturianu",@"Asturian"},{@"Български",@"Bulgarian"},{@"Català",@"Catalan"},{@"Čeština",@"Czech"},{@"Cymraeg",@"Welsh"},{@"Dansk",@"Danish"},{@"Deutsch",@"German"},{@"Ελληνικά",@"Greek"},{@"Australian English",@"Australian English"},{@"Canadian English",@"Canadian English"},{@"English (UK)",@"British English"},{@"Pirate Speak (PIRATE)",@"Pirate English"},{@"English (US)",@"American English"},{@"Esperanto (Mondo)",@"Esperanto"},{@"Español (Ar)",@"Argentinian Spanish"},{@"Español (Es)",@"Spanish"},{@"Español (Me)",@"Mexican Spanish"},{@"Español (Ur)",@"Uruguayan Spanish"},{@"Español (Ve)",@"Venezuelan Spanish"},{@"Eesti",@"Estonian"},{@"Euskara",@"Basque"},{@"Suomi",@"Finnish"},{@"Français (Fr)",@"French"},{@"Français (Ca)",@"Canadian French"},{@"Gaeilge",@"Irish"},{@"Galego",@"Galician"},{@"עברית",@"Hebrew"},{@"हिन्दी",@"Hindi"},{@"Hrvatski",@"Croatian"},{@"Magyar",@"Hungarian"},{@"Հայերեն",@"Armenian"},{@"Bahasa Indonesia",@"Indonesian"},{@"Íslenska",@"Icelandic"},{@"Italiano",@"Italian"},{@"日本語",@"Japanese"},{@"ქართული",@"Georgian"},{@"한국어",@"Korean"},{@"Kernowek",@"Cornwall"},{@"Lingua Latina",@"Latin"},{@"Lëtzebuergesch",@"Luxembourgish"},{@"Lietuvių",@"Lithuanian"},{@"Latviešu",@"Latvian"},{@"Bahasa Melayu",@"Malay"},{@"Malti",@"Maltese"},{@"Nederlands",@"Dutch"},{@"Norsk Nynorsk",@"Norwegian"},{@"Norsk",@"Norwegian"},{@"Occitan",@"Occitan"},{@"Polski",@"Polish"},{@"Português (Br)",@"Brazilian Portuguese"},{@"Portuguese (Po)",@"Portuguese"},{@"Quenya",@"Quenya (High Elvish)"},{@"Română",@"Romanian"},{@"Русский",@"Russian"},{@"Slovenčina",@"Slovak"},{@"Slovenščina",@"Slovenian"},{@"Српски",@"Serbian"},{@"Svenska",@"Swedish"},{@"ภาษาไทย",@"Thai"},{@"tlhIngan Hol",@"Klingon"},{@"Türkçe",@"Turkish"},{@"Українська",@"Ukrainian"},{@"Valencià",@"Valencian"},{@"Tiếng Việt",@"Vietnamese"},{@"简体中文",@"Chinese (Simplified)"},{@"繁體中文",@"Chinese (Traditional)"}

        };
        public  Dictionary<string,string> NameToFile = new Dictionary<string, string>
        {
           {@"Afrikaans",@"af_ZA.lang"},{@"Arabic",@"ar_SA.lang"},{@"Asturian",@"as_ES.lang"},{@"Bulgarian",@"bg_BG.lang"},{@"Catalan",@"ca_ES.lang"},{@"Czech",@"cs_CZ.lang"},{@"Welsh",@"cy_GB.lang"},{@"Danish",@"da_DK.lang"},{@"German",@"de_DE.lang"},{@"Greek",@"el_GR.lang"},{@"Australian English",@"en_AU.lang"},{@"Canadian English",@"en_CA.lang"},{@"British English",@"en_GB.lang"},{@"Pirate English",@"en_PT.lang"},{@"American English",@"en_US.lang"},{@"Esperanto",@"eo_UY.lang"},{@"Argentinian Spanish",@"es_AR.lang"},{@"Spanish",@"es_ES.lang"},{@"Mexican Spanish",@"es_MX.lang"},{@"Uruguayan Spanish",@"es_UY.lang"},{@"Venezuelan Spanish",@"es_VE.lang"},{@"Estonian",@"et_EE.lang"},{@"Basque",@"eu_ES.lang"},{@"Finnish",@"fi_FI.lang"},{@"French",@"fr_FR.lang"},{@"Canadian French",@"fr_CA.lang"},{@"Irish",@"ga_IE.lang"},{@"Galician",@"gl_ES.lang"},{@"Hebrew",@"he_IL.lang"},{@"Hindi",@"hi_IN.lang"},{@"Croatian",@"hr_HR.lang"},{@"Hungarian",@"hu_HU.lang"},{@"Armenian",@"hy_AM.lang"},{@"Indonesian",@"id_ID.lang"},{@"Icelandic",@"is_IS.lang"},{@"Italian",@"it_IT.lang"},{@"Japanese",@"ja_JP.lang"},{@"Georgian",@"ka_GE.lang"},{@"Korean",@"ko_KR.lang"},{@"Cornwall",@"kw_GB.lang"},{@"Latin",@"la_VA.lang"},{@"Luxembourgish",@"lb_LU.lang"},{@"Lithuanian",@"lt_LT.lang"},{@"Latvian",@"lv_LV.lang"},{@"Malay",@"ms_MY.lang"},{@"Maltese",@"mt_MT.lang"},{@"Dutch",@"nl_NL.lang"},{@"Norwegian",@"nb_NO.lang"},{@"Occitan",@"cc_CT.lang"},{@"Polish",@"pl_PL.lang"},{@"Brazilian Portuguese",@"pt_BR.lang"},{@"Portuguese",@"pt_PT.lang"},{@"Quenya (High Elvish)",@"qya_AA.lang"},{@"Romanian",@"ro_RO.lang"},{@"Russian",@"ru_RU.lang"},{@"Slovak",@"sk_SK.lang"},{@"Slovenian",@"sl_SI.lang"},{@"Serbian",@"sr_SP.lang"},{@"Swedish",@"sv_SE.lang"},{@"Thai",@"th_TH.lang"},{@"Klingon",@"tlh_AA.lang"},{@"Turkish",@"tr_TR.lang"},{@"Ukrainian",@"uk_UA.lang"},{@"Valencian",@"va_ES.lang"},{@"Vietnamese",@"vi_VN.lang"},{@"Chinese (Simplified)",@"zh_CN.lang"},{@"Chinese (Traditional)",@"zh_TW.lang"}

        };
        
        public static string getFileName(string name)
        {
            MCLocalize localiser = new MCLocalize();
            if (localiser.LocalNameToName.ContainsKey(name))
                return localiser.NameToFile[localiser.LocalNameToName[name]];
            if (localiser.NameToFile.ContainsKey(name))
                return localiser.NameToFile[name];
            return "en_GB.lang";
        }
    }
}
