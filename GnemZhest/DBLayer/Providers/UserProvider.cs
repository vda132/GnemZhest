using DBLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Providers;

public class UserProvider : Provider, IUserProvider
{
    public UserProvider(IConfiguration configuration) : base(configuration) { }

    public UserProvider(string connectionString) : base(connectionString) { }

    public async Task<bool> AddAsync(User model)
    {
        if (!await ValidateUserAsync(model))
            return false;

        await using var context = this.ZhestContext;
        await context.Users.AddAsync(model);
        await context.SaveChangesAsync();   

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var context = this.ZhestContext;

        var user = await GetUserAsync(id, context);

        if (user is null)
            return false;

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<IReadOnlyCollection<User>?> GetAllAsync()
    {
        await using var context = this.ZhestContext;
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetAsync(int id)
    {
        await using var context = this.ZhestContext;
        return await GetUserAsync(id, context);
    }

    public async Task<bool> UpdateAsync(int id, User model)
    {
        await using var context = this.ZhestContext;

        var user = await GetUserAsync(id, context);

        if (user is null)
            return false;

        user.Name = model.Name;
        user.SurName = model.SurName;
        user.Login = model.Login;
        user.Password = model.Password;
        user.Email = model.Email;
        user.Phone = model.Phone;
        user.Role = model.Role;

        context.Users.Update(user);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        await using var context = this.ZhestContext;
        return await context.Users.FirstOrDefaultAsync(x => x.Login.Equals(login));
    }

    private static async Task<User?> GetUserAsync(int id, ZhestContext context) =>
        await context.Users.FindAsync(id);

    private async Task<bool> ValidateUserAsync(User model)
    {
        if (model is null)
            return false;

        if (string.IsNullOrEmpty(model.Name) ||
            string.IsNullOrEmpty(model.SurName) ||
            string.IsNullOrEmpty(model.Login) ||
            string.IsNullOrEmpty(model.Password) ||
            string.IsNullOrEmpty(model.Email) ||
            string.IsNullOrEmpty(model.Phone) ||
            string.IsNullOrEmpty(model.Role))
            return false;

        await using var context = this.ZhestContext;

        if (await context.Users.FindAsync(model.Id) is not null)
            return false;

        if (await context.Users.AnyAsync(x => x.Login.Equals(model.Login) ||
                                         x.Phone.Equals(model.Phone) ||
                                         x.Email.Equals(model.Email)))
            return false;

        return true;
    }
}

