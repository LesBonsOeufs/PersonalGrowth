using System.Collections.Generic;
using System;

[Serializable]
public struct Model_RRuleType
{
    public int freq;
    public int interval;
    public string dtstart;
    public string until;
    public List<string> byweekday;

    public Model_RRuleType(int freq = -1, int interval = 1, string dtstart = "", string until = "", List<string> byweekday = default)
    {
        this.freq = freq;
        this.interval = interval;
        this.dtstart = dtstart == null ? DateTime.MinValue.ToString() : dtstart;
        this.until = until;
        this.byweekday = byweekday;
    }
}