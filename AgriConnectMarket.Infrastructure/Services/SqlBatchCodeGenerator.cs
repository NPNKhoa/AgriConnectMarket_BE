using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.ValueObjects;
using AgriConnectMarket.Infrastructure.Data;
using AgriConnectMarket.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class SqlBatchCodeGenerator : IBatchCodeGenerator
    {
        private readonly AppDbContext _db;

        public SqlBatchCodeGenerator(AppDbContext db)
        {
            _db = db;
        }

        public async Task<BatchCode> GenerateNextCodeAsync(string prefix, CancellationToken ct = default)
        {
            prefix = prefix?.Trim() ?? throw new ArgumentNullException(nameof(prefix));

            var strategy = _db.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _db.Database.BeginTransactionAsync(ct);

                var seq = await _db.Set<BatchCodeSequence>()
                    .FromSqlRaw("""
                        SELECT * 
                        FROM BatchCodeSequences 
                        WITH (UPDLOCK, ROWLOCK) 
                        WHERE Prefix = {0}
                    """, prefix)
                    .AsTracking()
                    .FirstOrDefaultAsync(ct);

                if (seq == null)
                {
                    seq = new BatchCodeSequence
                    {
                        Prefix = prefix,
                        LastNumber = 1
                    };
                    _db.Set<BatchCodeSequence>().Add(seq);
                }
                else
                {
                    seq.LastNumber++;
                }

                await _db.SaveChangesAsync(ct);

                await tx.CommitAsync(ct);

                return BatchCode.FromParts(prefix, seq.LastNumber);
            });
        }
    }
}
