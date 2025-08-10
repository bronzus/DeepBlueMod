AttackBoostBuff = {
    attackPowerMul = 1.8, 
    attackPowerBonusId = ""
}

function AttackBoostBuff:OnBuffInitDown()
    if (self.buff.isServer)then
        self.attackPowerBonusId = self.buff.characterCore:AddAttackPowerBonusOnServer(self.attackPowerMul)
    end
end 

function AttackBoostBuff:OnDestroy()
    if (self.buff.isServer)then
        self.buff.characterCore:RemoveAttackPowerBonusOnServer(self.attackPowerBonusId)
    end
end

