﻿namespace SunCorp.WebServerApplication.Services
{
    using Entities.Generic;
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entities;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEntitiesServer" in both code and config file together.

    [ServiceContract]
    public interface IEntitiesServer
    {
        [OperationContract]
        UsUsuarios GetUsUsuarios(UserSession session);

        [OperationContract]
        List<UsZona> GetListUsZona();

        [OperationContract]
        List<UsZona> GetListUsZonaPageList(int page, int numRows, ref int totalRows, bool includeDelete);

        [OperationContract]
        List<UsZona> GetListUsZonasUser(UsUsuarios user);

        [OperationContract]
        UsZona NewRegUsZona(UsZona zona);

        [OperationContract]
        bool UpdateRegUsZona(UsZona zona);

        [OperationContract]
        bool DeleteRegUsZona(UsZona zona);

        [OperationContract]
        UsTipoUsuario GetTypeUser(UsUsuarios user);
    }
}
