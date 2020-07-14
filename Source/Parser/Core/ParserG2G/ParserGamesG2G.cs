using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Parser.Core.ParserG2G
{
    class ParserGamesG2G 
    {
        private HashSet<string> urlSet;

        Dictionary<string, string> mouthDic = new Dictionary<string, string>()
        {
            {"January","01"},
            {"February","02"},
            {"March","03"},
            {"April","04"},
            {"May","05"},
            {"June","06"},
            {"July","07"},
            {"August","08"},
            {"September","09"},
            {"October","10"},
            {"November","11"},
            {"December","12"},
        };

        public Dictionary<string, Dictionary<string, string>> SynchronDictionary = new Dictionary<string, Dictionary<string, string>>()
            {
                {
                    "World of Warcraft", new Dictionary<string, string>()
                    {
                        { "Король-лич (Lich King)","Lich King"},
                        { "Пиратская бухта (Booty Bay)","Booty Bay"},
                        { "Подземье (Deephome)","Deepholm"},
                        { "Азурегос (Azuregos)","Azuregos"},
                        { "Разувий (Razuvious)","Razuvious"},
                        { "Ревущий фьорд (Howling Fjord)","Howling Fjord"},
                        { "Страж Смерти (Deathguard)","Deathguard"},
                        { "Свежеватель Душ (Soulflayer)","Soulflayer"},
                        { "Седогрив (Greymane)","Greymane"},
                        { "Ткач Смерти (Deathweaver)","Deathweaver"},
                        { "Термоштепсель (Thermaplugg)","Thermaplugg"},
                        { "Черный Шрам (Blackscar)","Blackscar"},
                        { "Ясеневый лес (Ashenvale) ","Ashenvale"},
                        { "Борейская тундра (Borean Tundra)","Borean Tundra"},
                        { "Вечная Песня (Eversong)","Eversong"},
                        { "Гром (Grom)","Grom"},
                        { "Галакронд (Galakrond)","Galakrond"},
                        { "Гордунни (Gordunni)","Gordunni"},
                        { "Голдринн (Goldrinn) ","Goldrinn"},
                        { "Дракономор (Fordragon)","Fordragon"},
                    }
                },
                {
                    "World of Warcraft Classic", new Dictionary<string, string>()
                    {
                        {"Classic - Anathema", "Anathema"},
                        {"Classic - Arcanite Reaper", "Arcanite Reaper"},
                        {"Classic - Arugal", "Arugal"},
                        {"Classic - Ashkandi", "Ashkandi"},
                        {"Classic - Atiesh", "Atiesh"},
                        {"Classic - Azuresong", "Azuresong"},
                        {"Classic - Benediction", "Benediction"},
                        {"Classic - Bigglesworth", "Bigglesworth"},
                        {"Classic - Blaumeux", "Blaumeux"},
                        {"Classic - Bloodsail Buccaneers", "Bloodsail Buccaneers"},
                        {"Classic - Deviate Delight", "Deviate Delight"},
                        {"Classic - Earthfury", "Earthfury"},
                        {"Classic - Faerlina", "Faerlina"},
                        {"Classic - Fairbanks", "Fairbanks"},
                        {"Classic - Felstriker", "Felstriker"},
                        {"Classic - Grobbulus", "Grobbulus"},
                        {"Classic - Heartseeker", "Heartseeker"},
                        {"Classic - Herod", "Herod"},
                        {"Classic - Incendius", "Incendius"},
                        {"Classic - Kirtonos", "Kirtonos"},
                        {"Classic - Kromcrush", "Kromcrush"},
                        {"Classic - Kurinnaxx", "Kurinnaxx"},
                        {"Classic - Loatheb", "Loatheb"},
                        {"Classic - Mankrik", "Mankrik"},
                        {"Classic - Myzrael", "Myzrael"},
                        {"Classic - Netherwind", "Netherwind"},
                        {"Classic - Old Blanchy", "Old Blanchy"},
                        {"Classic - Pagle", "Pagle"},
                        {"Classic - Rattlegore", "Rattlegore"},
                        {"Classic - Remulos", "Remulos"},
                        {"Classic - Skeram", "Skeram"},
                        {"Classic - Smolderweb", "Smolderweb"},
                        {"Classic - Stalagg", "Stalagg"},
                        {"Classic - Sul’thraze", "Sul’thraze"},
                        {"Classic - Sulfuras", "Sulfuras"},
                        {"Classic - Thalnos", "Thalnos"},
                        {"Classic - Thunderfury", "Thunderfury"},
                        {"Classic - Westfall", "Westfall"},
                        {"Classic - Whitemane", "Whitemane"},
                        {"Classic - Windseeker", "Windseeker"},
                        {"Classic - Yojamba", "Yojamba"},
                        {"Classic - Amnennar", "Amnennar"},
                        {"Classic - Ashbringer", "Ashbringer"},
                        {"Classic - Auberdine", "Auberdine"},
                        {"Classic - Bloodfang", "Bloodfang"},
                        {"Classic - Celebras", "Celebras"},
                        {"Classic - Dragon's Call", "Dragon's Call"},
                        {"Classic - Dragonfang", "Dragonfang"},
                        {"Classic - Dreadmist", "Dreadmist"},
                        {"Classic - Earthshaker", "Earthshaker"},
                        {"Classic - Everlook", "Everlook"},
                        {"Classic - Finkle", "Finkle"},
                        {"Classic - Firemaw", "Firemaw"},
                        {"Classic - Flamelash", "Flamelash"},
                        {"Classic - Gandling", "Gandling"},
                        {"Classic - Gehennas", "Gehennas"},
                        {"Classic - Golemagg", "Golemagg"},
                        {"Classic - Heartstriker", "Heartstriker"},
                        {"Classic - Hydraxian Waterlords", "Hydraxian Waterlords"},
                        {"Classic - Judgement", "Judgement"},
                        {"Classic - Lakeshire", "Lakeshire"},
                        {"Classic - Lucifron", "Lucifron"},
                        {"Classic - Mandokir", "Mandokir"},
                        {"Classic - Mirage Raceway", "Mirage Raceway"},
                        {"Classic - Mograine", "Mograine"},
                        {"Classic - Nethergarde Keep", "Nethergarde Keep"},
                        {"Classic - Noggenfogger", "Noggenfogger"},
                        {"Classic - Patchwerk", "Patchwerk"},
                        {"Classic - Pyrewood Village", "Pyrewood Village"},
                        {"Classic - Razorfen", "Razorfen"},
                        {"Classic - Razorgore", "Razorgore"},
                        {"Classic - Shazzrah", "Shazzrah"},
                        {"Classic - Skullflame", "Skullflame"},
                        {"Classic - Stonespine", "Stonespine"},
                        {"Classic - Sulfuron", "Sulfuron"},
                        {"Classic - Ten Storms", "Ten Storms"},
                        {"Classic - Transcendence", "Transcendence"},
                        {"Classic - Venoxis", "Venoxis"},
                        {"Classic - Zandalar Tribe", "Zandalar Tribe"},

                        {"Classic - Harbinger of Doom", "Harbinger of Doom (Вестник Рока)" },
                        {"Classic - Пламегор (Flamegor)", "Flamegor (Пламегор)" },
                        {"Classic - Рок-Делар (Rhok’delar)", "Rhok’delar (Рок-Делар)" },
                        {"Classic - Хроми (Chromie)", "Chromie (Хроми)" },
                        {"Classic - Змейталак (Wyrmthalak)", "Wyrmthalak (Змейталак)" },
                    }
                },
                {
                    "World of Tanks", new Dictionary<string, string>()
                    {
                        {"PC - EU", "(EU)"},
                        {"PC - US", "(US)"},
                        {"PC - SEA", "(SEA)"},
                        {"PC - RU", "(RU)"},
                        {"PS4 - EU", "PS4"},
                        {"PS4 - US", "PS4"},
                        {"Xbox One - All", "Xbox"},
                        {"Xbox 360 - All", "Xbox"},
                    }
                },
                {
                    "World of Kings", new Dictionary<string, string>()
                    {
                        {"[US] S1 - Skarabare", "(AM) S1-Skarabare"},
                        {"[US] S10 - Black Castle", "(AM) S10-Black Castle"},
                        {"[US] S11 - Dragonblight", "(AM) S11-Dragonblight"},
                        {"[US] S12 - Olaus Circle", "(AM) S12-Olaus Circle"},
                        {"[US] S13 - Burning Gate", "(AM) S13-Burning Gate"},
                        {"[US] S14 - Sanctuary", "(AM) S14-Sanctuary"},
                        {"[US] S15 - Farwatch", "(AM) S15-Farwatch"},
                        {"[US] S16 - Emerald Lake", "(AM) S16-Emerald Lake"},
                        {"[US] S2 - Dawnville", "(AM) S2-Dawnville"},
                        {"[US] S3 - Darkwood", "(AM) S3-Darkwood"},
                        {"[US] S4 - Frost Hill", "(AM) S4-Frost Hill"},
                        {"[US] S5 - Sunset Wild", "(AM) S5-Sunset Wild"},
                        {"[US] S6 - Uru Post", "(AM) S6-Uru Post"},
                        {"[US] S7 - Mooreda", "(AM) S7-Mooreda"},
                        {"[US] S8 - North Fort", "(AM) S8-North Fort"},
                        {"[US] S9 - Crate City", "(AM) S9-Crete City"},
                        {"[EU] S1&nbsp;-&nbsp;Uru&nbsp;Mooreda", "(EU) S1-Uru Mooreda"},
                        {"[EU] S2&nbsp;-&nbsp;Cyphersith", "(EU) S2-Cyphersith"},
                        {"[EU] S3&nbsp;-&nbsp;Crusader", "(EU) S3-Crusader"},
                        {"[EU] S4&nbsp;-&nbsp;Desmond", "(EU) S4-Desmond"},
                        {"[EU] S5&nbsp;-&nbsp;Ladanis", "(EU) S5-Ladanis"},
                        {"[EU] S6&nbsp;-&nbsp;Vioterian", "(EU) S6-Vioterian"},
                        {"[EU] S7&nbsp;-&nbsp;Augustinus", "(EU) S7-Augustinus"},
                        {"[EU] S8&nbsp;-&nbsp;Wanda", "(EU) S8-Wanda"},
                        {"[EU] S9&nbsp;-&nbsp;Eldrick", "(EU) S9-Eldrick"},
                        {"[EU] S10&nbsp;-&nbsp;Hadesfate", "(EU) S10-Hadesfate"},
                        {"[EU] S11&nbsp;-&nbsp;Windrider", "(EU) S11-Windrider"},
                        {"[EU] S12&nbsp;-&nbsp;Stormfury", "(EU) S12-Stormfury"},
                        {"[EU] S13&nbsp;-&nbsp;Hawkins", "(EU) S13-Hawkins"},
                        {"[EU] S14&nbsp;-&nbsp;Balmain", "(EU) S14-Balmain"},
                        {"[EU] S15&nbsp;-&nbsp;Sacrofice", "(EU) S15-Sacrofice"},
                        {"[EU] S16&nbsp;-&nbsp;Harbinger", "(EU) S16-Harbinger"},
                    }
                },
                {
                    "Warframe", new Dictionary<string, string>()
                    {
                        {"PS4", "[Прочие]" },
                        {"PC", "Warframe.com" },
                    }
                },
                {
                    "Warspear Online", new Dictionary<string, string>()
                    {
                        {"[EU] Emerald", "(EU) Emerald" },
                        {"[US] Sapphire", "(US) Sapphire" },
                        {"[RU] Amber", "(RU) Amber" },
                        {"[RU] Ruby", "(RU) Ruby" },
                        {"[RU] Topaz", "(RU) Topaz" },
                        {"[BR] Tourmaline", "(BR) Tourmaline" },
                        {"[SA] Pearl", "(SEA) Pearl" },
                        {"Xbox One", "Xbox"},
                    }
                },
                {
                    "Trove", new Dictionary<string, string>()
                    {
                        {"Kaiator", "PC (US) - Kaiator [PvP]"},
                        {"Velika", "PC (US) - Velika [PvE]"},
                        {"[PS4] Darkan", "PS4 (NA) - Darkan"},
                        {"[Xbox One] Lakan", "Xbox One (NA) - Lakan"},
                        {"[EN] Killian", "PC (EU) - Killian [PvP]"},
                        {"[EN] Mystel", "PC (EU) - Mystel [PvE]"},
                        {"[DE] Yurian", "PC (EU) - Yurian [PvE]"},
                        {"[PS4] Meldita", "PS4 (EU) - Meldita"},
                    }
                },
                {
                    "TERA", new Dictionary<string, string>()
                    {
                        {"[EU] Emerald", "(EU) Emerald" },
                        {"[US] Sapphire", "(US) Sapphire" },
                        {"[RU] Amber", "(RU) Amber" },
                        {"[RU] Ruby", "(RU) Ruby" },
                        {"[RU] Topaz", "(RU) Topaz" },
                        {"[BR] Tourmaline", "(BR) Tourmaline" },
                        {"[SA] Pearl", "(SEA) Pearl" },
                        {"Xbox One", "Xbox"},
                    }
                },
                {
                    "Tom Clancy's The Division", new Dictionary<string, string>()
                    {
                        {"PC", "(PC) The Division 2" },
                        {"PlayStation 4", "[Прочие] The Division 2" },
                        {"Xbox One", "(Xbox One) The Division 2" },
                    }
                },
                {
                    "Star Wars: TOR", new Dictionary<string, string>()
                    {
                        {"Sith Empire", "Empire" },
                        {"Galactic Republic", "Republic" },
                    }
                },
                {
                    "SoulWorker", new Dictionary<string, string>()
                    {
                        {"EN - Candus", "(EN) Candus" },
                        {"NA - Tenebris", "(NA) Tenebris" }
                    }
                },
                {
                    "RuneScape", new Dictionary<string, string>()
                    {
                        {"Runescape 3", "RuneScape" },
                        {"Deadman", "Old School RuneScape" },
                        {"Old school", "Old School RuneScape" },
                    }
                },
                {
                    "Rosh Online", new Dictionary<string, string>()
                    {
                        {"Asmara", "Axeso5.com Asmara"},
                    }
                },
                {
                    "Icarus", new Dictionary<string, string>()
                    {
                        {"[EU] Akaldus", "(EU) Akaldus" },
                        {"[EU] Koroshimo", "(EU) Koroshimo" },
                        {"[EU] Raken", "(EU) Raken" },
                        {"[EU] Velzeroth", "(EU) Velzeroth" },
                        {"[US] Baellas", "(US) Baellas" },
                        {"[US] Parna", "(US) Parna" },
                        {"[US] Radan", "(US) Radan" },
                        {"[US] Teleo", "(US) Teleo" },
                    }
                },
                {
                    "Revelation", new Dictionary<string, string>()
                    {
                        {"[NA] Darkfall", "(NA) Darkfall" },
                        {"[NA] Snowpine", "(NA) Snowpine" },
                        {"[EN] Moonsea", "(EU) Moonsea" },
                        {"[EN] Tidewater", "(EU) Tidewater" },
                    }
                },
                {
                    "Path of Exile", new Dictionary<string, string>()
                    {
                        {"[PC] Hardcore", "(PC) Hardcore"},
                        {"[PC] Standard", "(PC) Standard"},
                        {"[PC] Delirium Hardcore", "(PC) Hardcore Delirium"},
                        {"[PC] Delirium Standard", "(PC) Delirium"},
                        {"[PS4] Hardcore", "(PS4) Hardcore"},
                        {"[PS4] Standard", "(PS4) Standard"},
                        {"[PS4] Delirium Hardcore", "(PS4) Hardcore Delirium"},
                        {"[PS4] Delirium Standard", "(PS4) Standard"},
                        {"[Xbox] Hardcore", "(Xbox) Hardcore"},
                        {"[Xbox] Standard", "(Xbox) Standard"},
                        {"[Xbox] Delirium Hardcore", "(Xbox) Hardcore Delirium"},
                        {"[Xbox] Delirium Standard", "(Xbox) Delirium"},
                    }
                },
                {
                    "NHL#1", new Dictionary<string, string>()
                    {
                        {"PlayStation 4", "NHL 20 Ultimate Team (PS4)" },
                        {"Xbox One", "NHL 20 Ultimate Team (Xbox One)" },
                    }
                },
                {
                    "NHL#2", new Dictionary<string, string>()
                    {
                        {"PlayStation 4", "NHL 19 Ultimate Team (PS4)" },
                        {"Xbox One", "NHL 19 Ultimate Team (Xbox One)" },
                    }
                },
                {
                    "Neverwinter", new Dictionary<string, string>()
                    {
                        {"PS4", "PS4 (price per 1000)" },
                        {"Xbox One", "Xbox" },
                        {"PC - Dragon", "Neverwinter-world.ru (Classic)" },
                    }
                },
                {
                    "Rappelz", new Dictionary<string, string>()
                    {
                        {"US - Reviac", "(US) Reviac" },
                        {"US - Unicorn", "(US) Unicorn" },

                    }
                },
                {
                    "MU Legend", new Dictionary<string, string>()
                    {
                        {"[NA] Sezak ", "(NA) Sezak" },
                        {"[EU] Zephiros", "(EU) Zephiros" },
                        {"[SEA] Ohrdor", "(SEA) Ohrdor" },
                    }
                },
                {
                    "Perfect World", new Dictionary<string, string>()
                    {
                    }
                },
                {
                    "Metin2", new Dictionary<string, string>()
                    {
                    }
                },
                {
                    "Lost Ark", new Dictionary<string, string>()
                    {
                        {"[RU] -  Кратос", "(RU) Кратос" },
                        {"[RU] - Антарес", "(RU) Антарес" },
                        {"[RU] - Сирион", "(RU) Сирион" },
                    }
                },
                {
                    "The Lord of the Rings Online", new Dictionary<string, string>()
                    {
                        {"[EU] Belegaer", "Belegaer (DE-RP)" },
                        {"[EU] Evernight", "Evernight (EU)" },    
                        {"[EU] Gwaihir", "Gwaihir (DE)" },
                        {"[EU] Laurelin", "Laurelin (EN-RP)" },
                        {"[EU] Sirannon", "Sirannon (FR)" },
                        {"[US] Anor", "Anor - Legendary Server" },
                        {"[US] Arkenstone", "Arkenstone (US)" },
                        {"[US] Brandywine", "Brandywine (US)" },
                        {"[US] Crickhollow", "Crickhollow (US)" },
                        {"[US] Gladden", "Gladden (US)" },
                        {"[US] Landroval", "Landroval (EN-RE)" },
                    }
                },
                {
                    "Lineage 2", new Dictionary<string, string>()
                    {
                        {"EU - Core", "(EU) Core +Ramona" },
                        {"Chronos", "Chronos" },
                        {"Naia", "Naia" },
                        {"Classic US - Giran", "Classic US - Giran" },
                        {"Classic US - Talking Island", "Classic US - Talking Island" },
                    }
                },
                {
                    "League of Legends", new Dictionary<string, string>()
                    {
                        {"North America", "North America" },
                        {"EU West", "EU West" },
                        {"EU Nordic & East", "EU Nordic & East" },
                        {"Latin America North", "Latin America North" },
                        {"Latin America South", "Latin America South" },
                        {"Brazil", "Brazil" },
                        {"Japan", "Japan" },
                        {"Russia", "Russia" },
                        {"Turkey", "Turkey" },
                        {"Oceania", "Oceania" },
                        {"Republic of Korea", "Korea" },
                        {"Public Beta Environment", "PBE" },
                    }
                },
                {
                    "Heroes of the Storm", new Dictionary<string, string>()
                    {
                    }
                },
                {
                    "Hearthstone", new Dictionary<string, string>()
                    {
                        {"Asia", "Asia" },
                        {"Americas", "America" },
                        {"Europe", "Europe" },
                    }
                },
                {
                    "Guild Wars 2", new Dictionary<string, string>()
                    {
                    }
                },
                {
                    "GTA 5", new Dictionary<string, string>()
                    {
                        {"PC", "PC" },
                        {"Social Club", "Social Club" },
                        {"Xbox 360", "Xbox 360" },
                        {"Xbox One", "Xbox One" },
                        {"PlayStation 3", "[Прочие]" },
                        {"PlayStation 4", "[Прочие]" },
                    }
                },
                {
                    "Final Fantasy XIV Online", new Dictionary<string, string>()
                    {
                        {"Adamantoise", "(NA) Adamantoise"},
                        {"Balmung", "(NA) Balmung"},
                        {"Behemoth", "(NA) Behemoth"},
                        {"Brynhildr", "(NA) Brynhildr"},
                        {"Cactuar", "(NA) Cactuar"},
                        {"Cerberus", "(EU) Cerberus"},
                        {"Coeurl", "(NA) Coeurl"},
                        {"Diabolos", "(NA) Diabolos"},
                        {"Excalibur", "(NA) Excalibur"},
                        {"Exodus", "(NA) Exodus"},
                        {"Faerie", "(NA) Faerie"},
                        {"Famfrit", "(NA) Famfrit"},
                        {"Gilgamesh", "(NA) Gilgamesh"},
                        {"Goblin", "(NA) Goblin"},
                        {"Hyperion", "(NA) Hyperion"},
                        {"Jenova", "(NA) Jenova"},
                        {"Lamia", "(NA) Lamia"},
                        {"Leviathan", "(NA) Leviathan"},
                        {"Lich", "(EU) Lich"},
                        {"Louisoix", "(EU) Louisoix"},
                        {"Malboro", "(NA) Malboro"},
                        {"Mateus", "(NA) Mateus"},
                        {"Midgardsormr", "(NA) Midgardsormr"},
                        {"Moogle", "(EU) Moogle"},
                        {"Odin", "(EU) Odin"},
                        {"Omega", "(EU) Omega"},
                        {"Phoenix", "(EU) Phoenix"},
                        {"Ragnarok", "(EU) Ragnarok"},
                        {"Sargatanas", "(NA) Sargatanas"},
                        {"Shiva", "(EU) Shiva"},
                        {"Siren", "(NA) Siren"},
                        {"Spriggan", "(EU) Spriggan"},
                        {"Twintania", "(EU) Twintania"},
                        {"Ultros", "(NA) Ultros"},
                        {"Zalera", "(NA) Zalera"},
                        {"Zodiark", "(EU) Zodiark"},

                    }
                },
                {
                    "Forza Horizon", new Dictionary<string, string>()
                    {
                        {"PC", "(PC) Forza Horizon 4" },
                        {"Xbox One", "(Xbox One) Forza Horizon 4" },
                    }
                },
                {
                    "FIFA#1", new Dictionary<string, string>()
                    {
                        {"IOS", "FIFA Mobile" },
                        {"Android", "FIFA Mobile" },
                    }
                },
                {
                    "FIFA#2", new Dictionary<string, string>()
                    {
                        {"PC", "FIFA 20 Ultimate Team (PC)" },
                        {"Nintendo Switch", "FIFA 20 Ultimate Team [Прочие]" },
                        {"PlayStation 4", "FIFA 20 Ultimate Team [Прочие]" },
                        {"Xbox One", "FIFA 20 Ultimate Team (Xbox One)" },
                    }
                },
                {
                    "FIFA#3", new Dictionary<string, string>()
                    {
                        {"PC", "FIFA 19 Ultimate Team (PC)" },
                        {"Nintendo Switch", "FIFA 19 Ultimate Team [Прочие]" },
                        {"PlayStation 4", "FIFA 19 Ultimate Team [Прочие]" },
                        {"Xbox One", "FIFA 19 Ultimate Team (Xbox One)" },
                    }
                },
                {
                    "FIFA#4", new Dictionary<string, string>()
                    {
                        {"PC", "FIFA 18 Ultimate Team (PC)" },
                        {"Nintendo Switch", "FIFA 18 Ultimate Team [Прочие]" },
                        {"PlayStation 4", "FIFA 18 Ultimate Team [Прочие]" },
                        {"Xbox One", "FIFA 18 Ultimate Team (Xbox One)" }
                    }
                },
                {
                    "EVE Online", new Dictionary<string, string>()
                    {
                    }
                },
                {
                    "Escape from Tarkov", new Dictionary<string, string>()
                    {
                        {"PC", "Pattern" },
                    }
                },
                {
                    "Diablo 3#1", new Dictionary<string, string>()
                    {
                        {"Hardcore", "America (Battle.net, PC)" },
                        {"Hardcore - Season", "America (Battle.net, PC)" },
                        {"Normal", "America (Battle.net, PC)" },
                        {"Normal - Season", "America (Battle.net, PC)" },
                    }
                },
                {
                    "Diablo 3#2", new Dictionary<string, string>()
                    {
                        {"Hardcore", "Europe (Battle.net, PC)" },
                        {"Hardcore - Season", "Europe (Battle.net, PC)" },
                        {"Normal", "Europe (Battle.net, PC)" },
                        {"Normal - Season", "Europe (Battle.net, PC)" },
                    }
                },
                {
                    "Dragon Nest", new Dictionary<string, string>()
                    {
                        {"[US] Rajuul", "(US) Rajuul" },
                    }
                },
                {
                    "Cabal Online", new Dictionary<string, string>()
                    {
                        {"Mercury", "(EU) Mercury" },
                        {"Venus", "(EU) Venus" },
                        {"Titan", "(US) Titan" },
                    }
                },
                {
                    "Blade & Soul", new Dictionary<string, string>()
                    {
                        {"Jinsoyun", "Europe (EU)" },
                        {"Yura", "North America (NA)" },
                    }
                },
                {
                    "Black Desert", new Dictionary<string, string>()
                    {
                        {"Europe", "(EU)" },
                        {"North America", "(NA)" },
                        {"SEA", "(SEA)" },
                        {"South America", "(SA)" },
                        {"Pattern", "[PS4]" },
                        {"Taiwan", "[Прочие]" },
                        {"Russia", "Эллиан (F2P)" },
                        {"MENA", "[Прочие]" },
                        {"Thailand", "[Прочие]" },
                        {"[PS4] EU", "[PS4]" },
                        {"[PS4] NA", "[PS4]" },
                        {"[Xbox] EU", "[Xbox]" },
                        {"[Xbox] NA", "[Xbox]" },
                    }
                },
                {
                    "Astellia Online", new Dictionary<string, string>()
                    {
                        {"EU - Aquarius", "(EU) Aquarius" },
                        {"US - Aida", "(NA) Aida" },
                    }
                },
                {
                    "ArcheAge: Unchained", new Dictionary<string, string>()
                    {
                        {"EU - Alexander", "(EU) Alexander" },
                        {"EU - Belstrom", "(EU) Belstrom" },
                        {"EU - Gildaron", "(EU) Gildaron" },
                        {"EU - Halnaak", "(EU) Halnaak" },
                        {"EU - Tinnereph", "(EU) Tinnereph" },
                        {"US - Denistrious", "(NA) Denistrious" },
                        {"US - Jergant", "(NA) Jergant" },
                        {"US - Kaylin", "(NA) Kaylin" },
                        {"US - Runert", "(NA) Runert" },
                        {"US - Tyrenos", "(NA) Tyrenos" },
                        {"US - Wynn", "(NA) Wynn" },
                    }
                },
                {
                    "ArcheAge", new Dictionary<string, string>()
                    {
                        {"Aria", "(NA) Aria" },
                        {"Kadum", "(NA) Kadum" },
                        {"Nui", "(NA) Nui - Fresh Start" },
                        {"Ezi", "(EU) Ezi - Fresh Start" },
                        {"Jakar", "(EU) Jakar" },
                        {"Taris", "(EU) Taris" },
                    }
                },
                {
                    "APB: Reloaded", new Dictionary<string, string>()
                    {
                        {"Citadel (EU)", "(EU) Citadel" },
                        {"Jericho (NA)", "(NA) Jericho" },   
                    }
                },
                {
                    "Anthem", new Dictionary<string, string>()
                    {
                        {"PC", "PC" },
                        {"PlayStation 4", "[Прочие]" },
                        {"Xbox One", "Xbox" },
                    }
                },
                {
                    "Albion Online", new Dictionary<string, string>()
                    {
                        {"Main Server", "Albion (live)" },
                    }
                },
                {
                    "Aion", new Dictionary<string, string>()
                    {
                        {"Danaria", "(NA) Danaria" },
                        {"Ereshkigal", "(NA) Ereshkigal" },
                        {"Katalam", "(NA) Katalam" },
                        {"[INTL] Sillus", "(EN) Sillus" },
                        {"[INTL] Stormwing", "(EN) Stormwing" },
                        {"[DE] Odin", "(DE) Odin" },
                        {"[FR] Ragnarok", "(FR) Ragnarok" },
                    }
                },
                {
                    "Conqueror's Blade", new Dictionary<string, string>()
                    {
                        {"North America", "North America" },
                        {"Europe East", "Europe East" },
                        {"Europe West", "Europe West" },
                        {"Europe West 2", "Europe West 2" },
                    }
                },
            };

        private void GetUnderCategories(IElement item, string gameName, out string under1, out string under2)
        {
            string[] str = new string[5].Select((x) => x ="-").ToArray();
            var descriptionItems = item.QuerySelectorAll("span.products__description-item");
            int i = 0;
            foreach (var descriptionItem in descriptionItems)
            {
                if (!descriptionItem.TextContent.Contains("Method") && !descriptionItem.TextContent.Contains("Type") && !descriptionItem.TextContent.Contains("Tier"))
                {
                    var info = descriptionItem.QuerySelector("span.products__description-info");
                    if (info != null)
                    {
                        string sourceCategory = info.TextContent.Replace("\n", "").Trim();
                        string descCategory;
                        if (SynchronDictionary[gameName].TryGetValue(sourceCategory, out descCategory))
                            str[i] = descCategory;
                        else
                            str[i] = sourceCategory;

                        if (info.TextContent.Contains("Server"))
                        {
                            string tmp = str[i];
                            str[i] = str[0];
                            str[0] = str[i];
                        }

                        i++;
                    }
                }
            }

            under1 = str[0];
            under2 = str[1];
        }

        private void GetDescriptionAmountCost(IElement item, out string description, out int amount, out double cost, out string priceTitle)
        {
            amount = -1;
            description = item.QuerySelector("div.products__main-info-right a.products__name").
                TextContent.Replace("\n", "").Trim();

            cost = 1/ExchangeRate.ExchangeRate.Eur;

            if (String.IsNullOrEmpty(description))
            {
                var str = item.QuerySelector("span.products__exch-rate").TextContent;
                amount = int.Parse(string.Join(String.Empty, str.Where(c => char.IsDigit(c))));
                description = "-";
                cost *= double.Parse(item.QuerySelector("span.products__exch-rate span[id^='ppu']").TextContent);
                if (str.Contains("Mil ") || str.Contains("Bil "))
                {
                    priceTitle = @"Price/1kk";
                    cost *= 1;
                }
                else if (str.Contains("K "))
                {
                    priceTitle = @"Price/1000";
                    cost *= 1;
                }
                else
                {
                    priceTitle = @"Price/1000";
                    cost *= double.Parse(item.QuerySelector("span.products__exch-rate span[id^='ppu']").TextContent) * 1000;
                }
            }
            else
            {
                priceTitle = "Price";
                cost *= double.Parse(item.QuerySelector("span[id^='amount']").TextContent);
            }

            cost = Math.Round(cost, 5);
        }

        private string GetRegisteredTime(IElement item, DateTime now)
        {
            string result;
            var dataItem = item.QuerySelector("span.tooltip__content span");
            
            if (dataItem == null) return "Recently";

            string dateStr = dataItem.TextContent;

            DateTime regDate = DateTime.ParseExact(dateStr,"MMMM d, yyyy", CultureInfo.InvariantCulture);
            int days = (int)((now - regDate).TotalDays);

            int years = days / 365;
            if (years == 0)
            {
                int months = days / 30;
                if (months == 0)
                {
                    result = (days == 1) ? days + " day" : days + " days";
                }
                else
                {
                    result = (months == 1) ? months + " month" : months + " months";
                }
            }
            else
            {
                result = (years == 1) ? years + " year" : years + " years";
            }
            return result;
        }

        public int GetReviewsCount(IElement item) =>
            int.Parse(item.QuerySelector("span.seller__level-icon-counter").TextContent);

        public void Parse(IHtmlDocument document, string gameNameWithId, string category, string serverLocation, int count)
        {
            urlSet = new HashSet<string>();

            string gameName = gameNameWithId.Split('#')[0];
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var items = document.QuerySelectorAll("ul#product-listings li.products__list-item");
            int counter = 0;
            foreach (var item in items)
            {
                if (count == counter) return;

                string url = ((IHtmlAnchorElement)item.QuerySelector("span.products__num a")).Href.Replace("https://www.","");

                if (!urlSet.Add(url)) return;

                DateTime now = DateTime.Now;
                string nick = item.QuerySelector("a.seller__name").TextContent.Replace(" ","").Replace("\n","");
                GetUnderCategories(item, gameNameWithId, out var serverName, out var side);
                GetDescriptionAmountCost(item, out var description, out var amount, out var cost, out var priceTitle);
                bool isOnline = item.QuerySelector("span.seller__status").ClassName.Contains("online");
                string dateRegisteredTime = GetRegisteredTime(item, now);
                int revCount = GetReviewsCount(item);

                var x = new GameInfo(
                    DateTime.Now,
                    DateTime.Now,
                    url,
                    gameName,
                    serverLocation,
                    category,
                    serverName,
                    side,
                    amount,
                    cost,
                    description,
                    nick,
                    revCount,
                    dateRegisteredTime,
                    priceTitle,
                    isOnline);

                lock (Program.Locker)
                    Form1.testArr.Add(x);

                counter++;
            }

        }

    }
}
