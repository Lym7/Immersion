﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;

namespace Immersion
{
    public class BlockMortarAndPestle : Block
    {
        public override bool DoParticalSelection(IWorldAccessor world, BlockPos Pos)
        {
            return true;
        }

        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (world.BlockAccessor.GetBlock(blockSel.Position.UpCopy()).Id != 0) return false;

            if (blockSel.SelectionBoxIndex != 4)
            {
                BlockEntityMortarAndPestle beQuern = world.BlockAccessor.GetBlockEntity(blockSel.Position) as BlockEntityMortarAndPestle;
                if (beQuern != null && beQuern.CanGrind())
                {
                    beQuern.SetPlayerGrinding(byPlayer, true);
                    return true;
                }
            }

            return base.OnBlockInteractStart(world, byPlayer, blockSel);
        }

        public override bool OnBlockInteractStep(float secondsUsed, IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            BlockEntityMortarAndPestle beQuern = world.BlockAccessor.GetBlockEntity(blockSel.Position) as BlockEntityMortarAndPestle;
            if (beQuern != null)
            {
                return beQuern.CanGrind();
            }

            return false;
        }

        public override void OnBlockInteractStop(float secondsUsed, IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            BlockEntityMortarAndPestle beQuern = world.BlockAccessor.GetBlockEntity(blockSel.Position) as BlockEntityMortarAndPestle;
            if (beQuern != null)
            {
                beQuern.SetPlayerGrinding(byPlayer, false);
            }

        }

        public override bool OnBlockInteractCancel(float secondsUsed, IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel, EnumItemUseCancelReason cancelReason)
        {
            BlockEntityMortarAndPestle beQuern = world.BlockAccessor.GetBlockEntity(blockSel.Position) as BlockEntityMortarAndPestle;
            if (beQuern != null)
            {
                beQuern.SetPlayerGrinding(byPlayer, false);
            }


            return true;
        }

    }
}
