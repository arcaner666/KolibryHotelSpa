using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Constants;
using BusinessLayer.Utilities.Results;
using DataAccessLayer.Abstract;
using Entities.ConfigurationModels;
using Entities.DatabaseModels;
using Entities.DTOs;

namespace BusinessLayer.Concrete;

public class ClaimBl : IClaimBl
{
    private readonly IClaimDal _claimDal;
    private readonly IMapper _mapper;

    public ClaimBl(
        IClaimDal claimDal,
        IMapper mapper
    )
    {
        _mapper = mapper;
        _claimDal = claimDal;
    }

    public IDataResult<List<ClaimDto>> GetAll()
    {
        List<Claim> claims = _claimDal.GetAll();
        if (!claims.Any())
            return new ErrorDataResult<List<ClaimDto>>(Messages.ClaimsNotFound);

        var claimDtos = _mapper.Map<List<ClaimDto>>(claims);

        return new SuccessDataResult<List<ClaimDto>>(claimDtos, Messages.ClaimsListed);
    }

    public IDataResult<ClaimDto> GetByTitle(string title)
    {
        Claim claim = _claimDal.GetByTitle(title);
        if (claim is null)
            return new ErrorDataResult<ClaimDto>(Messages.ClaimNotFound);

        var claimDto = _mapper.Map<ClaimDto>(claim);

        return new SuccessDataResult<ClaimDto>(claimDto, Messages.ClaimListedByTitle);
    }
}
