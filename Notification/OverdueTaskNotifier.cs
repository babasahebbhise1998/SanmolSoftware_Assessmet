using Microsoft.EntityFrameworkCore;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.DataContext;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Notification;
using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;

public class OverdueTaskNotifier : BackgroundService
{
    private readonly IServiceProvider _sp;
    private readonly ILogger<OverdueTaskNotifier> _log;
    private readonly TimeSpan _period = TimeSpan.FromHours(24);
    public OverdueTaskNotifier(IServiceProvider sp, ILogger<OverdueTaskNotifier> log)
    {
        _sp = sp;
        _log = log;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                using var scope = _sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDataContext>();
                var email = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var today = DateTime.Today;

                var overdue = await db.TaskItems
                                      .Include(t => t.Customer)
                                      .Where(t => t.DueDate < today &&
                                                  t.Status == TaskCurrentStatus.Pending)
                                      .ToListAsync(ct);

                foreach (var t in overdue)
                {
                    var body = $"""
                        <h4>Task over‑due!</h4>
                        <p><strong>{t.Description}</strong> for customer <b>{t.Customer.Name}</b><br/>
                           was due on {t.DueDate:yyyy‑MM‑dd}.</p>
                        """;

                     email.SendAsync(
                        to: "babasaheb.bhise1998@gmail.com",
                        subject: "⏰ Over‑due task reminder",
                        htmlBody: body);
                }

                await db.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Over‑due notifier error");
            }
            await Task.Delay(_period, ct);
        }
    }
}
