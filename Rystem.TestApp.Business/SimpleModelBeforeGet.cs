using RepositoryFramework;
using Rystem.TestApp.Domain;

namespace Rystem.TestApp.Business
{
    public class SimpleModelBeforeGet : IRepositoryBusinessBeforeGet<SimpleModel, string>
    {
        private readonly IRepositoryPattern<SimpleModel, string> _repositoryPattern;

        public int Priority => 2;
        public SimpleModelBeforeGet(IRepositoryPattern<SimpleModel, string> repositoryPattern)
        {
            _repositoryPattern = repositoryPattern;
        }
        public async Task<State<SimpleModel, string>> BeforeGetAsync(string key, CancellationToken cancellationToken = default)
        {
            //do something before the get, for instance I can check the existence of the entity using
            //the repository pattern interface (an interface without business integration)
            //I need to use an IRepositoryPattern to avoid the loop for business concatenation requests.
            if (await _repositoryPattern.ExistAsync(key, cancellationToken))
            {
                return State<SimpleModel, string>.Ok();
            }
            else
                return State<SimpleModel, string>.NotOk();
        }
    }
}