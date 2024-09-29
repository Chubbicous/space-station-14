using Content.Shared.Silicons.Laws;
using Content.Shared.Containers.ItemSlots;
using JetBrains.Annotations;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;
using Robust.Shared.Prototypes;

namespace Content.Shared.Silicons.Laws
{
    [UsedImplicitly]
    public abstract class SharedAiUploadConsoleSystem : EntitySystem
    {
        [Dependency] private readonly ItemSlotsSystem _itemSlotsSystem = default!;
        [Dependency] private readonly ILogManager _log = default!;

        public const string Sawmill = "uploadconsole";
        protected ISawmill _sawmill = default!;

        public override void Initialize()
        {
            base.Initialize();
            _sawmill = _log.GetSawmill(Sawmill);

            SubscribeLocalEvent<AiUploadConsoleComponent, ComponentInit>(OnComponentInit);
            SubscribeLocalEvent<AiUploadConsoleComponent, ComponentRemove>(OnComponentRemove);
        }

        private void OnComponentInit(EntityUid uid, AiUploadConsoleComponent component, ComponentInit args)
        {
            _itemSlotsSystem.AddItemSlot(uid, AiUploadConsoleComponent.AiUploadModuleSlot1, component.ModuleSlot1);
            _itemSlotsSystem.AddItemSlot(uid, AiUploadConsoleComponent.AiUploadModuleSlot2, component.ModuleSlot2);
            // _itemSlotsSystem.AddItemSlot(uid, AiUploadConsoleComponent.PrivilegedIdCardSlotId, component.PrivilegedIdSlot);
            // _itemSlotsSystem.AddItemSlot(uid, AiUploadConsoleComponent.TargetIdCardSlotId, component.TargetIdSlot);
        }

        private void OnComponentRemove(EntityUid uid, AiUploadConsoleComponent component, ComponentRemove args)
        {
            _itemSlotsSystem.RemoveItemSlot(uid, component.ModuleSlot1);
            _itemSlotsSystem.RemoveItemSlot(uid, component.ModuleSlot2);
            // _itemSlotsSystem.RemoveItemSlot(uid, component.PrivilegedIdSlot);
            // _itemSlotsSystem.RemoveItemSlot(uid, component.TargetIdSlot);
        }

        [Serializable, NetSerializable]
        private sealed class AiUploadConsoleComponentState : ComponentState
        {
            public List<string> AccessLevels;

            public AiUploadConsoleComponentState(List<string> accessLevels)
            {
                AccessLevels = accessLevels;
            }
        }
    }
}
