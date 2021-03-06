﻿using NSI_CRK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NSI_CRK.DAL
{
    public class AbsencesRepository : Repository<Absence>, IAbsencesRepository
    {
        public AbsencesRepository(CRKContext context) : base(context)
        { }

        public IEnumerable<Absence> GetFilteredAbsences(string SearchString = null)
        {
            var absences = crkContext.Absences.AsQueryable();
            if (!String.IsNullOrEmpty(SearchString))
            {
                var toUpper = SearchString.ToUpper();
                absences = crkContext.Absences.Where(a => (a.Employee.FirstName.ToString() + " " + a.Employee.LastName.ToString()).Contains(toUpper) ||
                                                    a.Type.ToString().Contains(toUpper));
            }
            return absences;
        }
    }
}