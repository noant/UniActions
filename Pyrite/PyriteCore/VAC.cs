﻿
namespace PyriteCore
{
    internal static class VAC
    {
        public static class AppSettingsNames
        {
            public static readonly string ActionsPorts = "action_ports";
            public static readonly string DistributionPort = "serverPort";
            public static readonly string UsedActionCustomSettings = "use_actioncustomsettings";
            public static readonly string UsedActionServerCommand = "use_actionservercommand";
            public static readonly string UsedCustomAction = "use_action";
            public static readonly string UsedUseServerCommand = "use_servercommand";
            public static readonly string UsedActionName = "use_actionname";
            public static readonly string UsedCategory = "use_category";
            public static readonly string UsedIsActive = "use_isactive";
            public static readonly string UsedOffOnState = "use_offonstate";
            public static readonly string UsedIndex = "use_index";
            public static readonly string ActionModule = "actionmodule";
            public static readonly string CheckerModule = "checkermodule";
            public static readonly string ResolvedIp = "resolvedip";
            public static readonly string ResolveAll = "resolveall";
            public static readonly string Action = "action";
        }

        public static class ServerCommands
        {
            public static readonly string Command_GetStartCommands = "getallcommands";
            public static readonly string Command_GetCategoryCommands = "getcategorycommands";
            public static readonly string Command_EndFastActions = "endfastactions";

            public static readonly string Command_NeedUpdate = "needUpdate";
            public static readonly string Command_Except = "except";
            public static readonly string Command_All = "all";

            public static readonly string Command_GetStatus = "getstate";
        }
    }
}