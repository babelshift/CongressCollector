using AutoMapper;
using CongressCollector.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongressCollector.Models
{
    public class SubjectTypeConverter : ITypeConverter<Original.Subjects, Cleaned.BillSubject>
    {
        public Cleaned.BillSubject Convert(Original.Subjects source, Cleaned.BillSubject destination, ResolutionContext context)
        {
            List<Cleaned.LegislativeSubject> legislativeSubjects = new List<Cleaned.LegislativeSubject>();

            if (source != null 
                && source.BillSubjects != null
                && source.BillSubjects.LegislativeSubjects != null 
                && source.BillSubjects.LegislativeSubjects.Items != null)
            {
                legislativeSubjects = source.BillSubjects.LegislativeSubjects.Items
                    .Select(x => new Cleaned.LegislativeSubject() { Name = x.Name }).ToList();
            }

            Cleaned.BillSubject billSubject = new Cleaned.BillSubject()
            {
                LegislativeSubjects = legislativeSubjects.ToList().AsReadOnly(),
                PolicyArea = context.Mapper.Map<Original.PolicyArea, Cleaned.PolicyArea>(source.BillSubjects.PolicyArea)
            };

            return billSubject;
        }
    }
}
