using SuperAdminService.Data.Entities;

namespace SuperAdminService.Data.Interfaces
{
    public interface IRole
    {
        List<Role> GetRoles();
        Role GetRole(int id);
        bool AddRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(int id);
    }
}
