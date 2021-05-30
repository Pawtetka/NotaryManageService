using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NotaryDatabaseDLL.Models;
using NotaryService.Business.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Implementation.Services
{
    public class DocumentsService : ICrudInterface<Document>
    {
        private readonly NotaryOfficeContext _context;
        private readonly AbstractValidator<Document> _validator;
        public DocumentsService(NotaryOfficeContext context, AbstractValidator<Document> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task CreateEntityAsync(Document model)
        {
            await _validator.ValidateAsync(model);
            await _context.Documents.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Document>> GetAllAsync(Expression<Func<Document, bool>> filter = null)
        {
            var entities = _context.Documents.AsQueryable();
            if (filter != null) entities = entities.Where(filter);

            return await entities.ToListAsync();
        }

        public async Task<Document> GetByIdAsync(int id)
        {
            return await _context.Documents.FirstOrDefaultAsync(m => m.DocumentId == id);
        }

        public async Task<IEnumerable<Document>> GetEntitiesByPrincipalId(int principalId)
        {
            return await _context.Documents.AsQueryable().Where(a => a.DocumentId.Equals(principalId)).ToListAsync();
        }

        public async Task UpdateEntity(Document model)
        {
            var entity = await _context.Documents.FindAsync(model.DocumentId);
            await _validator.ValidateAsync(model);

            entity.DocumentName = model.DocumentName;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
