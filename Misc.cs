using UnityEngine;
using Il2CppQuantum;
using Il2CppView_Humanoid;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace AFUtils;

public static class Misc
{
    private static Humanoid_View localHumanoidView;

    public static Humanoid_View GetLocalHumanoidView()
    {
        if (localHumanoidView != null) return localHumanoidView;

        Il2CppArrayBase<Humanoid_View> views = GameObject.FindObjectsOfType<Humanoid_View>();
        foreach (var view in views)
        {
            if (view.isLocal)
            {
                localHumanoidView = view;
                return view;
            }
        }
        return null;
    }

    public static void PrintComponentList(EntityRef r)
    {
        var game = QuantumRunner.Default?.Game;
        if (game == null) return;
        Frame frame = game.Frames.Verified;
        var sb = new System.Text.StringBuilder($"\nEntity '{r}' components:\n");

        if (frame.Has<Transform2D>(r)) sb.Append("Il2CppQuantum.Transform2D\n");
        if (frame.Has<Transform3D>(r)) sb.Append("Il2CppQuantum.Transform3D\n");
        if (frame.Has<Transform2DVertical>(r)) sb.Append("Il2CppQuantum.Transform2DVertical\n");
        if (frame.Has<PhysicsCollider2D>(r)) sb.Append("Il2CppQuantum.PhysicsCollider2D\n");
        if (frame.Has<PhysicsBody2D>(r)) sb.Append("Il2CppQuantum.PhysicsBody2D\n");
        if (frame.Has<PhysicsCollider3D>(r)) sb.Append("Il2CppQuantum.PhysicsCollider3D\n");
        if (frame.Has<PhysicsBody3D>(r)) sb.Append("Il2CppQuantum.PhysicsBody3D\n");
        if (frame.Has<PhysicsJoints2D>(r)) sb.Append("Il2CppQuantum.PhysicsJoints2D\n");
        if (frame.Has<PhysicsJoints3D>(r)) sb.Append("Il2CppQuantum.PhysicsJoints3D\n");
        if (frame.Has<PhysicsCallbacks2D>(r)) sb.Append("Il2CppQuantum.PhysicsCallbacks2D\n");
        if (frame.Has<PhysicsCallbacks3D>(r)) sb.Append("Il2CppQuantum.PhysicsCallbacks3D\n");
        if (frame.Has<CharacterController2D>(r)) sb.Append("Il2CppQuantum.CharacterController2D\n");
        if (frame.Has<CharacterController3D>(r)) sb.Append("Il2CppQuantum.CharacterController3D\n");
        if (frame.Has<NavMeshPathfinder>(r)) sb.Append("Il2CppQuantum.NavMeshPathfinder\n");
        if (frame.Has<NavMeshSteeringAgent>(r)) sb.Append("Il2CppQuantum.NavMeshSteeringAgent\n");
        if (frame.Has<NavMeshAvoidanceAgent>(r)) sb.Append("Il2CppQuantum.NavMeshAvoidanceAgent\n");
        if (frame.Has<NavMeshAvoidanceObstacle>(r)) sb.Append("Il2CppQuantum.NavMeshAvoidanceObstacle\n");
        if (frame.Has<View>(r)) sb.Append("Il2CppQuantum.View\n");
        if (frame.Has<MapEntityLink>(r)) sb.Append("Il2CppQuantum.MapEntityLink\n");
        if (frame.Has<AffectedByRain>(r)) sb.Append("Il2CppQuantum.AffectedByRain\n");
        if (frame.Has<Airplane>(r)) sb.Append("Il2CppQuantum.Airplane\n");
        if (frame.Has<AirplaneTraffic>(r)) sb.Append("Il2CppQuantum.AirplaneTraffic\n");
        if (frame.Has<Ammo>(r)) sb.Append("Il2CppQuantum.Ammo\n");
        if (frame.Has<AntiYeetWorkaround>(r)) sb.Append("Il2CppQuantum.AntiYeetWorkaround\n");
        if (frame.Has<Armor>(r)) sb.Append("Il2CppQuantum.Armor\n");
        if (frame.Has<AutoAim>(r)) sb.Append("Il2CppQuantum.AutoAim\n");
        if (frame.Has<AutoAimTarget>(r)) sb.Append("Il2CppQuantum.AutoAimTarget\n");
        if (frame.Has<AutoResetButton>(r)) sb.Append("Il2CppQuantum.AutoResetButton\n");
        if (frame.Has<AutoTurret>(r)) sb.Append("Il2CppQuantum.AutoTurret\n");
        if (frame.Has<Axe>(r)) sb.Append("Il2CppQuantum.Axe\n");
        if (frame.Has<Bat>(r)) sb.Append("Il2CppQuantum.Bat\n");
        if (frame.Has<BigScreen>(r)) sb.Append("Il2CppQuantum.BigScreen\n");
        if (frame.Has<BigScreen_PongGame>(r)) sb.Append("Il2CppQuantum.BigScreen_PongGame\n");
        if (frame.Has<BigScreen_Vote>(r)) sb.Append("Il2CppQuantum.BigScreen_Vote\n");
        if (frame.Has<BlockFlag>(r)) sb.Append("Il2CppQuantum.BlockFlag\n");
        if (frame.Has<BoxingBagBase>(r)) sb.Append("Il2CppQuantum.BoxingBagBase\n");
        if (frame.Has<Caltrops>(r)) sb.Append("Il2CppQuantum.Caltrops\n");
        if (frame.Has<Car>(r)) sb.Append("Il2CppQuantum.Car\n");
        if (frame.Has<CarLODMinusOne>(r)) sb.Append("Il2CppQuantum.CarLODMinusOne\n");
        if (frame.Has<Chain>(r)) sb.Append("Il2CppQuantum.Chain\n");
        if (frame.Has<Chainsaw>(r)) sb.Append("Il2CppQuantum.Chainsaw\n");
        if (frame.Has<CheckPoint>(r)) sb.Append("Il2CppQuantum.CheckPoint\n");
        if (frame.Has<CheckpointSpawner>(r)) sb.Append("Il2CppQuantum.CheckpointSpawner\n");
        if (frame.Has<Collectable>(r)) sb.Append("Il2CppQuantum.Collectable\n");
        if (frame.Has<CollectablePickupPending>(r)) sb.Append("Il2CppQuantum.CollectablePickupPending\n");
        if (frame.Has<ConfettiCannon_Stationary>(r)) sb.Append("Il2CppQuantum.ConfettiCannon_Stationary\n");
        if (frame.Has<ConfettiGun>(r)) sb.Append("Il2CppQuantum.ConfettiGun\n");
        if (frame.Has<ConsumablesRespawner>(r)) sb.Append("Il2CppQuantum.ConsumablesRespawner\n");
        if (frame.Has<Controllable>(r)) sb.Append("Il2CppQuantum.Controllable\n");
        if (frame.Has<ConveyorBelt>(r)) sb.Append("Il2CppQuantum.ConveyorBelt\n");
        if (frame.Has<ConveyorBeltTempMovingTerrain>(r)) sb.Append("Il2CppQuantum.ConveyorBeltTempMovingTerrain\n");
        if (frame.Has<Crowbar>(r)) sb.Append("Il2CppQuantum.Crowbar\n");
        if (frame.Has<DependencyDespawn>(r)) sb.Append("Il2CppQuantum.DependencyDespawn\n");
        if (frame.Has<Despawn>(r)) sb.Append("Il2CppQuantum.Despawn\n");
        if (frame.Has<Door>(r)) sb.Append("Il2CppQuantum.Door\n");
        if (frame.Has<EMPgrenade>(r)) sb.Append("Il2CppQuantum.EMPgrenade\n");
        if (frame.Has<Electrocuted>(r)) sb.Append("Il2CppQuantum.Electrocuted\n");
        if (frame.Has<EntityProjectile>(r)) sb.Append("Il2CppQuantum.EntityProjectile\n");
        if (frame.Has<Equipment>(r)) sb.Append("Il2CppQuantum.Equipment\n");
        if (frame.Has<Explosion>(r)) sb.Append("Il2CppQuantum.Explosion\n");
        if (frame.Has<Fire>(r)) sb.Append("Il2CppQuantum.Fire\n");
        if (frame.Has<FloatOnOcean>(r)) sb.Append("Il2CppQuantum.FloatOnOcean\n");
        if (frame.Has<FoamBlob>(r)) sb.Append("Il2CppQuantum.FoamBlob\n");
        if (frame.Has<FreeMovingPickup>(r)) sb.Append("Il2CppQuantum.FreeMovingPickup\n");
        if (frame.Has<Grenade>(r)) sb.Append("Il2CppQuantum.Grenade\n");
        if (frame.Has<Gun>(r)) sb.Append("Il2CppQuantum.Gun\n");
        if (frame.Has<HangGlider>(r)) sb.Append("Il2CppQuantum.HangGlider\n");
        if (frame.Has<Healing>(r)) sb.Append("Il2CppQuantum.Healing\n");
        if (frame.Has<Health>(r)) sb.Append("Il2CppQuantum.Health\n");
        if (frame.Has<HeldByHumanoid>(r)) sb.Append("Il2CppQuantum.HeldByHumanoid\n");
        if (frame.Has<HeldByPickup>(r)) sb.Append("Il2CppQuantum.HeldByPickup\n");
        if (frame.Has<Horses>(r)) sb.Append("Il2CppQuantum.Horses\n");
        if (frame.Has<HoverBike>(r)) sb.Append("Il2CppQuantum.HoverBike\n");
        if (frame.Has<Humanoid>(r)) sb.Append("Il2CppQuantum.Humanoid\n");
        if (frame.Has<HumanoidOnMovingTerrain>(r)) sb.Append("Il2CppQuantum.HumanoidOnMovingTerrain\n");
        if (frame.Has<Jerrycan>(r)) sb.Append("Il2CppQuantum.Jerrycan\n");
        if (frame.Has<Kalashnikov>(r)) sb.Append("Il2CppQuantum.Kalashnikov\n");
        if (frame.Has<KillerTagTracker>(r)) sb.Append("Il2CppQuantum.KillerTagTracker\n");
        if (frame.Has<Laser>(r)) sb.Append("Il2CppQuantum.Laser\n");
        if (frame.Has<Machete>(r)) sb.Append("Il2CppQuantum.Machete\n");
        if (frame.Has<MeleeAttack>(r)) sb.Append("Il2CppQuantum.MeleeAttack\n");
        if (frame.Has<Minigun>(r)) sb.Append("Il2CppQuantum.Minigun\n");
        if (frame.Has<MovingHazard>(r)) sb.Append("Il2CppQuantum.MovingHazard\n");
        if (frame.Has<MovingTerrain>(r)) sb.Append("Il2CppQuantum.MovingTerrain\n");
        if (frame.Has<ParticipatingPlayer>(r)) sb.Append("Il2CppQuantum.ParticipatingPlayer\n");
        if (frame.Has<PathBlocker>(r)) sb.Append("Il2CppQuantum.PathBlocker\n");
        if (frame.Has<Pickup>(r)) sb.Append("Il2CppQuantum.Pickup\n");
        if (frame.Has<Pipe>(r)) sb.Append("Il2CppQuantum.Pipe\n");
        if (frame.Has<PlasmaBall>(r)) sb.Append("Il2CppQuantum.PlasmaBall\n");
        if (frame.Has<PlasmaBall_Erratic>(r)) sb.Append("Il2CppQuantum.PlasmaBall_Erratic\n");
        if (frame.Has<PlasmaGun>(r)) sb.Append("Il2CppQuantum.PlasmaGun\n");
        if (frame.Has<PlayArea>(r)) sb.Append("Il2CppQuantum.PlayArea\n");
        if (frame.Has<Player>(r)) sb.Append("Il2CppQuantum.Player\n");
        if (frame.Has<PoliceCoordinator>(r)) sb.Append("Il2CppQuantum.PoliceCoordinator\n");
        if (frame.Has<PoliceHelicopter>(r)) sb.Append("Il2CppQuantum.PoliceHelicopter\n");
        if (frame.Has<PoliceVehicleGunman>(r)) sb.Append("Il2CppQuantum.PoliceVehicleGunman\n");
        if (frame.Has<QuickStrike>(r)) sb.Append("Il2CppQuantum.QuickStrike\n");
        if (frame.Has<RaceGameState>(r)) sb.Append("Il2CppQuantum.RaceGameState\n");
        if (frame.Has<RaceParticipator>(r)) sb.Append("Il2CppQuantum.RaceParticipator\n");
        if (frame.Has<RainStorm>(r)) sb.Append("Il2CppQuantum.RainStorm\n");
        if (frame.Has<Rebar>(r)) sb.Append("Il2CppQuantum.Rebar\n");
        if (frame.Has<RebarGun>(r)) sb.Append("Il2CppQuantum.RebarGun\n");
        if (frame.Has<RetainOwnershipOnRespawn>(r)) sb.Append("Il2CppQuantum.RetainOwnershipOnRespawn\n");
        if (frame.Has<RiotShield>(r)) sb.Append("Il2CppQuantum.RiotShield\n");
        if (frame.Has<RiotStick>(r)) sb.Append("Il2CppQuantum.RiotStick\n");
        if (frame.Has<SandboxRespawn>(r)) sb.Append("Il2CppQuantum.SandboxRespawn\n");
        if (frame.Has<ShopState>(r)) sb.Append("Il2CppQuantum.ShopState\n");
        if (frame.Has<Shotgun>(r)) sb.Append("Il2CppQuantum.Shotgun\n");
        if (frame.Has<Shuriken>(r)) sb.Append("Il2CppQuantum.Shuriken\n");
        if (frame.Has<SkyYeetFinder>(r)) sb.Append("Il2CppQuantum.SkyYeetFinder\n");
        if (frame.Has<Sledgehammer>(r)) sb.Append("Il2CppQuantum.Sledgehammer\n");
        if (frame.Has<SleepWithJoint>(r)) sb.Append("Il2CppQuantum.SleepWithJoint\n");
        if (frame.Has<SnapToOffset>(r)) sb.Append("Il2CppQuantum.SnapToOffset\n");
        if (frame.Has<SpawnShop>(r)) sb.Append("Il2CppQuantum.SpawnShop\n");
        if (frame.Has<SpecialArenaBehavior>(r)) sb.Append("Il2CppQuantum.SpecialArenaBehavior\n");
        if (frame.Has<Spectator>(r)) sb.Append("Il2CppQuantum.Spectator\n");
        if (frame.Has<StartLineGate>(r)) sb.Append("Il2CppQuantum.StartLineGate\n");
        if (frame.Has<StickyWeapon>(r)) sb.Append("Il2CppQuantum.StickyWeapon\n");
        if (frame.Has<StraggleBoostSpawner>(r)) sb.Append("Il2CppQuantum.StraggleBoostSpawner\n");
        if (frame.Has<Stuck>(r)) sb.Append("Il2CppQuantum.Stuck\n");
        if (frame.Has<StunState>(r)) sb.Append("Il2CppQuantum.StunState\n");
        if (frame.Has<SwitchButton>(r)) sb.Append("Il2CppQuantum.SwitchButton\n");
        if (frame.Has<SwitchTriggeredMovingTerrain>(r)) sb.Append("Il2CppQuantum.SwitchTriggeredMovingTerrain\n");
        if (frame.Has<Television>(r)) sb.Append("Il2CppQuantum.Television\n");
        if (frame.Has<TrackAndShootAI>(r)) sb.Append("Il2CppQuantum.TrackAndShootAI\n");
        if (frame.Has<TrafficCarriageStaticDataComponent>(r)) sb.Append("Il2CppQuantum.TrafficCarriageStaticDataComponent\n");
        if (frame.Has<TrafficCarriageTag>(r)) sb.Append("Il2CppQuantum.TrafficCarriageTag\n");
        if (frame.Has<TrafficCarriages>(r)) sb.Append("Il2CppQuantum.TrafficCarriages\n");
        if (frame.Has<TrafficReactions>(r)) sb.Append("Il2CppQuantum.TrafficReactions\n");
        if (frame.Has<TrafficRoad>(r)) sb.Append("Il2CppQuantum.TrafficRoad\n");
        if (frame.Has<TrafficSign>(r)) sb.Append("Il2CppQuantum.TrafficSign\n");
        if (frame.Has<TrafficTruckStaticDataComponent>(r)) sb.Append("Il2CppQuantum.TrafficTruckStaticDataComponent\n");
        if (frame.Has<TrainTrack>(r)) sb.Append("Il2CppQuantum.TrainTrack\n");
        if (frame.Has<TramTrack>(r)) sb.Append("Il2CppQuantum.TramTrack\n");
        if (frame.Has<TransferJoints>(r)) sb.Append("Il2CppQuantum.TransferJoints\n");
        if (frame.Has<Tutorial>(r)) sb.Append("Il2CppQuantum.Tutorial\n");
        if (frame.Has<UnderWater>(r)) sb.Append("Il2CppQuantum.UnderWater\n");
        if (frame.Has<UniqueEquipment>(r)) sb.Append("Il2CppQuantum.UniqueEquipment\n");
        if (frame.Has<Vehicle>(r)) sb.Append("Il2CppQuantum.Vehicle\n");
        if (frame.Has<VictoryCelebration>(r)) sb.Append("Il2CppQuantum.VictoryCelebration\n");
        if (frame.Has<Wet>(r)) sb.Append("Il2CppQuantum.Wet\n");

        Core.Logger.Msg(sb.ToString());
    }
}