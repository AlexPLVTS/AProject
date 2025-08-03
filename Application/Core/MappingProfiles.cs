using System;
using System.Collections.Generic;
using Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Activity, Activity>();
    }
}
