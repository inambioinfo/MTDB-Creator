﻿namespace MTDBFramework.Data
{
    public interface ITargetFilter
    {
        Options FilterOptions { get; set; }

        bool ShouldFilter(Target target);
    }
}