using deepend.entity.Request;
using deepend.entity.Response;
using deepend.entity;

namespace deepend.business.Interface
{
    public interface IChequeComponent
    {
        ChequeResponse Create(ChequeRequest request);
        ChequeResponse Transform(ChequeResponse response);
        void ValidatEntity(EntityBase enttiy);
    }
}
