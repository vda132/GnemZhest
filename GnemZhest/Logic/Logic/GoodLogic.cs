using DBLayer.Providers;

namespace Logic.Logic;

public class GoodLogic : Logic<DBLayer.Models.Good>, IGoodLogic
{
    private readonly IGoodProvider provider;

    public GoodLogic(IGoodProvider provider) : base(provider) =>
        this.provider = provider;

    public override async Task<bool> DeleteAsync(int id)
    {
        var good = await this.provider.GetAsync(id);

        if (good is null)
            return false;

        good!.IsAvaliable = false;

        return await this.provider.UpdateAsync(id, good);
    }
}
