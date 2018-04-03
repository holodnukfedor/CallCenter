using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CallCenterDAL.Entities;

namespace CallCenterDAL.Context
{
    public class CallCenterDbInitializer : DropCreateDatabaseIfModelChanges<CallCenterDbContext>
    {
        private Random _random = new Random();

        private static int _phoneCallId = 3;

        private void AddPhoneCall(CallCenterDbContext context, PhoneCall call)
        {
            if (call.ConnectionTime != null)
                call.DurationSeconds = (int) (call.TerminationTime - (DateTime)call.ConnectionTime).TotalSeconds;

            context.PhoneCalls.Add(call);
        }

        private void AddRandomPhoneCall(CallCenterDbContext context, DateTime parentCallStartTime, int? parentCallId, bool needCreateRelativeCall = false)
        {
            int userId1 = _random.Next(1, 11);
            int userId2;
            do
            {
                userId2 = _random.Next(1, 11);
            } while (userId1 == userId2);

            PhoneCallStatus phoneCallStatus = (PhoneCallStatus)_random.Next(0, 5);

            DateTime startTime = parentCallStartTime - new TimeSpan(_random.Next(0, 24), _random.Next(0, 60), _random.Next(5, 60));

            PhoneCall call = new PhoneCall
            {
                Id = _phoneCallId,
                StartTime = startTime,
                Status = phoneCallStatus,
                ParentCallId = parentCallId
            };

            switch (phoneCallStatus)
            {
                case PhoneCallStatus.Connected:
                    call.ConnectionTime = startTime + new TimeSpan(0, _random.Next(0, 3), _random.Next(5, 60));
                    call.TerminationTime = (DateTime) call.ConnectionTime + new TimeSpan(0, _random.Next(0, 57), _random.Next(30, 60));
                    break;
                default:
                    call.TerminationTime = startTime + new TimeSpan(0, _random.Next(0, 5), _random.Next(30, 60));
                    break;
            }

            AddPhoneCall(context, call);

            context.UserInPhoneCalls.Add(new UserInPhoneCall
            {
                PhoneCallId = _phoneCallId,
                UserId = userId1,
                Status = UserInPhoneStatus.Called
            });

            switch (phoneCallStatus)
            {
                case PhoneCallStatus.Dropped:
                    context.UserInPhoneCalls.Add(new UserInPhoneCall
                    {
                        PhoneCallId = _phoneCallId,
                        UserId = userId2,
                        Status = UserInPhoneStatus.Dropped
                    });
                    break;
                case PhoneCallStatus.Missed:
                    context.UserInPhoneCalls.Add(new UserInPhoneCall
                    {
                        PhoneCallId = _phoneCallId,
                        UserId = userId2,
                        Status = UserInPhoneStatus.Missed
                    });
                    break;
                case PhoneCallStatus.Connected:
                    context.UserInPhoneCalls.Add(new UserInPhoneCall
                    {
                        PhoneCallId = _phoneCallId,
                        UserId = userId2,
                        Status = UserInPhoneStatus.Accepted
                    });
                    break;
                case PhoneCallStatus.Error:
                    context.UserInPhoneCalls.Add(new UserInPhoneCall
                    {
                        PhoneCallId = _phoneCallId,
                        UserId = userId2,
                        Status = UserInPhoneStatus.Error
                    });
                    break;
                case PhoneCallStatus.ErrorAfterConnection:
                    context.UserInPhoneCalls.Add(new UserInPhoneCall
                    {
                        PhoneCallId = _phoneCallId,
                        UserId = userId2,
                        Status = UserInPhoneStatus.Error
                    });
                    break;
                default:
                    break;
            }
            ++_phoneCallId;

            if (needCreateRelativeCall)
            {
                TimeSpan durability = startTime - call.TerminationTime;
                TimeSpan halfDurability = new TimeSpan(durability.Hours / 2, durability.Minutes / 2, durability.Seconds / 2);
                AddRandomPhoneCall(context, startTime + halfDurability, call.Id);
            }
        }

        protected override void Seed(CallCenterDbContext context)
        {
            context.Users.Add(new User
            {
                Id = 1,
                FirstName = "Федор",
                LastName = "Холоднюк",
                Patronymic = "Ярославович",
                Phone = "79203124068",
            });

            context.Users.Add(new User
            {
                Id = 2,
                FirstName = "Виталий",
                LastName = "Литвинов",
                Patronymic = "Алексеевич",
                Phone = "70973392508",
            });

            context.Users.Add(new User
            {
                Id = 3,
                FirstName = "Юрий",
                LastName = "Березенко",
                Patronymic = "Александрович",
                Phone = "70982546677",
            });

            context.Users.Add(new User
            {
                Id = 4,
                FirstName = "Иван",
                LastName = "Иванов",
                Patronymic = "Иванович",
                Phone = "70982546677",
            });

            context.Users.Add(new User
            {
                Id = 5,
                FirstName = "Радим",
                LastName = "Мамедов",
                Patronymic = "Вениаминович",
                Phone = "89412779351",
            });

            context.Users.Add(new User
            {
                Id = 6,
                FirstName = "Софон",
                LastName = "Вирский",
                Patronymic = "Егорович",
                Phone = "89244806718",
            });

            context.Users.Add(new User
            {
                Id = 7,
                FirstName = "Станислава",
                LastName = "Лапина",
                Patronymic = "Матвеевна",
                Phone = "89523338946",
            });

            context.Users.Add(new User
            {
                Id = 8,
                FirstName = "Панфил",
                LastName = "Власов",
                Patronymic = "Иванович",
                Phone = "89027221665",
            });

            context.Users.Add(new User
            {
                Id = 9,
                FirstName = "Порфирий",
                LastName = "Антонов",
                Patronymic = "Константинович",
                Phone = "89205452920",
            });


            context.Users.Add(new User
            {
                Id = 10,
                FirstName = "Никита",
                LastName = "Карпов",
                Patronymic = "Викторович",
                Phone = "89342663183",
            });

            AddPhoneCall(context, new PhoneCall
            {
                Id = 1,
                Status = PhoneCallStatus.Connected,
                StartTime = new DateTime(2018, 3, 29, 15, 58, 10),
                ConnectionTime = new DateTime(2018, 3, 29, 15, 58, 30),
                TerminationTime = new DateTime(2018, 3, 29, 15, 59, 30)
            });

            AddPhoneCall(context, new PhoneCall
            {
                Id = 2,
                Status = PhoneCallStatus.Connected,
                StartTime = new DateTime(2018, 3, 29, 16, 58, 10),
                ConnectionTime = new DateTime(2018, 3, 29, 16, 58, 30),
                TerminationTime = new DateTime(2018, 3, 29, 16, 59, 30)
            });

            context.UserInPhoneCalls.Add(new UserInPhoneCall
            {
                PhoneCallId = 1,
                UserId = 1,
                Status = UserInPhoneStatus.Called
            });

            context.UserInPhoneCalls.Add(new UserInPhoneCall
            {
                PhoneCallId = 1,
                UserId = 3,
                Status = UserInPhoneStatus.Accepted
            });

            context.UserInPhoneCalls.Add(new UserInPhoneCall
            {
                PhoneCallId = 2,
                UserId = 2,
                Status = UserInPhoneStatus.Called
            });

            context.UserInPhoneCalls.Add(new UserInPhoneCall
            {
                PhoneCallId = 2,
                UserId = 4,
                Status = UserInPhoneStatus.Accepted
            });

            _phoneCallId = 3;
            for (int i = 3; i < 16; ++i)
            {
                AddRandomPhoneCall(context, DateTime.Now, null, _random.Next(1, 11) % 3 == 0);
            }
        }
    }
}
