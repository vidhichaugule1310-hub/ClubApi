using ClubApi.Models;

namespace ClubApi.Services
{
    public interface IMemberService
    {
        List<Member> GetAll();
        Member Add(Member m);
        Guid InstanceId { get; }
    }
}
