module MiningPullMonitoring.Types

open System

type StatsData = {
    time: int64
    reportedHashrate: float
    currentHashrate: float
    averageHashrate: float
    activeWorkers: int
    coinsPerMin: double
    usdPerMin: double
    unpaid: UInt64
}

type CurrentStats = {
     status: string
     data: StatsData
     }