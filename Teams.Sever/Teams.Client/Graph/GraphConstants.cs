using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Client.Graph
{
    public class GraphConstants
    {

        public readonly static string[] DefaultScopes =
       {
            UserReadWrite,
            MailboxSettingsRead
        };


        public const string MailboxSettingsRead = "MailboxSettings.Read";


        public const string UserRead = "User.Read";
        public const string UserReadBasicAll = "User.ReadBasic.All";
        public const string UserReadAll = "User.Read.All";
        public const string UserReadWrite = "User.ReadWrite";
        public const string UserReadWriteAll = "User.ReadWrite.All";
        public const string GroupReadWriteAll = "Group.ReadWrite.All";

    }
}
