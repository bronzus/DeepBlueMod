SharkSkill = {
    useEffect = nil,
    sharkBodyMesh = nil,
    AttackBoostMul = 1.7
}

function SharkSkill:Awake()
    self.useEffect = self.skill:GetComponentInChildren(typeof(CS.UnityEngine.ParticleSystem))
end

function SharkSkill:OnSkillInitDown()
    sharkBodyMesh = self.skill.skillBuffCore.injectVariables:Get("bodyMeshObj"):GetComponent(typeof(CS.UnityEngine.SkinnedMeshRenderer))
    shape = self.useEffect.shape;
    shape.skinnedMeshRenderer = sharkBodyMesh
end

function SharkSkill:UseSkillByAI(ctx)
    self:UseSkillOnServer()
    self.skill:UsedByAI()
    ctx.Done = true
end

function SharkSkill:UseSkillWhenButtonDownLocalPlayer()
    self.skill:Command("CommandUseSkill", {})
    self.skill:UsedByPlayer()
end

function SharkSkill:UseSkillOnServer()
    self.skill.skillBuffCore:AddBuffOnServerByName("", "AttackBoostBuff", 12);
    self.skill:ClientRpc("ClientRpcPlayEffect", {})
end

function SharkSkill:GetSkillDesc()
    return "这是描述， 加" + AttackBoostMul * self.skill.level + "倍的攻击力"
end

function SharkSkill:CommandUseSkill(params)
    self:UseSkillOnServer()
end 

function SharkSkill:ClientRpcPlayEffect(params)
    self.useEffect:Play()
end 