using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.ValueObjects;
using AgriConnectMarket.Infrastructure.Data;
using AgriConnectMarket.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class SqlBatchCodeGenerator(AppDbContext _db) : IBatchCodeGenerator
    {
        public async Task<BatchCode> GenerateNextCodeAsync(string prefix, CancellationToken ct = default)
        {
            prefix = prefix?.Trim() ?? throw new ArgumentNullException(nameof(prefix));

            // Start a transaction — the SELECT ... WITH (UPDLOCK) will be part of it.
            await using var tx = await _db.Database.BeginTransactionAsync(ct);

            // Try to select the existing sequence row with update lock
            // Use FromSqlRaw to place locking hints. Map to OrderCodeSequence entity.
            var seq = await _db.Set<BatchCodeSequence>()
                .FromSqlRaw("SELECT * FROM OrderCodeSequences WITH (UPDLOCK, ROWLOCK) WHERE Prefix = {0}", prefix)
                .AsTracking()
                .FirstOrDefaultAsync(ct);

            if (seq == null)
            {
                // No row exists, insert new row with LastNumber = 1
                seq = new BatchCodeSequence { Prefix = prefix, LastNumber = 1 };
                _db.Set<BatchCodeSequence>().Add(seq);
                await _db.SaveChangesAsync(ct);
            }
            else
            {
                // Row exists, increment LastNumber
                seq.LastNumber++;
                await _db.SaveChangesAsync(ct);
            }

            await tx.CommitAsync(ct);

            // return formatted code
            return BatchCode.FromParts(prefix, seq.LastNumber);
        }
    }
}
