using ClubApi.Models;

namespace ClubApi.Services
{
    public class MemberService : IMemberService
    {
        private static readonly List<Member> _members = new();
        public Guid InstanceId { get; } = Guid.NewGuid();

        public List<Member> GetAll() => _members;

        public Member Add(Member m)
        {   
            m.Id = _members.Count + 1;
            _members.Add(m);
            return m;
        }
    }
}