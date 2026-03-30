using APDB_Cw1_s21417.Models;

namespace APDB_Cw1_s21417.Services;

public class UserService
{
    private readonly List<User> _users = new();

    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetById(int id) =>
        _users.FirstOrDefault(u => u.Id == id);

    public IReadOnlyList<User> GetAll() => _users;
}

