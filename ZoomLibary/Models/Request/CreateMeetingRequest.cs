using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomAPI.Models.Request
{
    public class CreateMeetingRequest
    {
        public string agenda { get; set; }
        public bool default_password { get; set; }
        public int duration { get; set; }
        public string password { get; set; }
        public bool pre_schedule { get; set; }
        public Recurrence recurrence { get; set; }
        public Settings settings { get; set; }
        public DateTime start_time { get; set; }
        public string template_id { get; set; }
        public string timezone { get; set; }
        public string topic { get; set; }
        public TrackingField tracking_fields { get; set; }
        public int type { get; set; }

    }
    public class TrackingField
    {
        public string field { get; set; }
        public string value { get; set; }
    }
    public class Recurrence {
        public DateTime end_date_time { get; set; }
        public int end_times { get; set; }
        public int monthly_day { get; set; }
        public int monthly_week { get; set; }
        public int monthly_week_day { get; set; }
        public int repeat_interval { get; set; }
        public int type { get; set; }
        /// <summary>
        /// 1 - Sunday.
        /// 2 - Monday.
        /// 3 - Tuesday.
        /// 4 - Wednesday.
        /// 5 - Thursday.
        /// 6 - Friday.
        /// 7 - Saturday.
        /// </summary>
        public int weekly_days { get; set; }
        public string schedule_for { get; set; }
    }
    public class Settings {
        public string[] additional_data_center_regions { get; set; }
        public bool allow_multiple_devices { get; set; }
        public string alternative_hosts { get; set; }
        public bool alternative_hosts_email_notification { get; set; }
        public int approval_type { get; set; }
        public ApprovedOrDeniedCountriesOrRegions approved_or_denied_countries_or_regions { get; set; }
        public string audio { get; set; }
        public string authentication_domains { get; set; }
        public AuthenticationException[] authentication_exception { get; set; }
        public string authentication_option { get; set; }
        public string auto_recording { get; set; }
        public BreakoutRoom breakout_room { get; set; }
        public int calendar_type { get; set; }
        public bool close_registration { get; set; }
        public string contact_email { get; set; }
        public string contact_name { get; set; }
        public bool email_notification { get; set; }
        public string encryption_type { get; set; }
        public bool focus_mode { get; set; }
        public string[] global_dial_in_countries { get; set; }
        public bool host_video { get; set; }
        public int jbh_time { get; set; }
        public bool join_before_host { get; set; }
        public LanguageInterpretation language_interpretation { get; set; }
        public bool meeting_authentication { get; set; }
        public MeetingInvite[] meeting_invitees { get; set; }
        public bool mute_upon_entry { get; set; }
        public bool participant_video { get; set; }
        public bool private_meeting { get; set; }
        public bool registrants_confirmation_email { get; set; }
        public bool registrants_email_notification { get; set; }
        public int registration_type { get; set; }
        public bool show_share_button { get; set; }
        public bool use_pmi { get; set; }
        public bool waiting_room { get; set; }
        public bool watermark { get; set; }
        public bool host_save_video_order { get; set; }
        public bool alternative_host_update_polls { get; set; }
    }
    public class ApprovedOrDeniedCountriesOrRegions {
        public string[] approved_list { get; set; }
        public string[] denied_list { get; set; }
        public bool enable { get; set; }
        public string method { get; set; }
    }
    public class AuthenticationException {
        public string email { get; set; }
        public string name { get; set; }
    }
    public class Room
    {
        public string name { get; set; }
        public string[] participants { get; set; }
    }
    public class BreakoutRoom {
        public bool enable { get; set; }
        public Room[] rooms { get; set; }
    }
    public class Interpreter
    {
        public string email { get; set; }
        public string languages { get; set; }
    }
    public class LanguageInterpretation
    {
        public bool enable { get; set; }
        public Interpreter[] interpreters { get; set; }
    }
    public class MeetingInvite
    {
        public string email { get; set; }
    }
}
