using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsTU.Model.Infrastructure
{
    public interface IUserContextProvider
    {
        UserContext GetCurrentUserContext();

        void SetCurrentUserContext(UserContext userContext);

        void ClearCurrentUserContext();
    }
}
