
            float4x4 rotate(float3 r, float4 d) // r=rotations axes
            {
                float cx, cy, cz, sx, sy, sz;
                sincos(r.x, sx, cx);
                sincos(r.y, sy, cy);
                sincos(r.z, sz, cz);
                return float4x4( cy*cz, -sz, sy, d.x,
                sz, cx*cz, -sx, d.y,
                -sy, sx, cx*cy, d.z,
                0, 0, 0, d.w );
            }

			float getTime(float stopTime, float timeOffset)
            {
            	float time = _Time.y + timeOffset;
            	if (stopTime >= 0) time = stopTime + timeOffset;
            	return time;
            }

            float4 SideMove(float4 vertexPos, float _sideDisplaceFreq,float _sideDisplaceCorrectFreq, float _sideDisplace, half maskedFactor, float stopTime, float timeOffset)
			{
				//side to side movement
            	float t = getTime(stopTime,timeOffset);
				vertexPos.x += -sin(t * _sideDisplaceFreq + _sideDisplaceCorrectFreq) * 0.001 * _sideDisplace * maskedFactor;
				return vertexPos;
			}

            float4 SpineYaw(float4 vertexPos, float spineYawSize,float spineYawLength, float spineYawFreq,float spineYawCorrectFreq, half maskedFactor, float stopTime, float timeOffset)
			{
            	float t = getTime(stopTime,timeOffset);
				float4x4 rot_mat1;
				rot_mat1 = rotate(float3(0,spineYawSize * sin(vertexPos.z * spineYawLength + (t * spineYawFreq + spineYawCorrectFreq)),0) * maskedFactor * 0.3,float4(0,0,0,1));
				vertexPos = mul(rot_mat1, vertexPos);
				return vertexPos;
			}

            float4 SpinePitch(float4 vertexPos, float spineYawSize, float spineYawLength, float spineYawFreq,float spineYawCorrectFreq, half maskedFactor, float stopTime,float timeOffset)
			{
            	float t = getTime(stopTime,timeOffset);
				float4x4 rot_mat1;
				rot_mat1 = rotate(float3(spineYawSize * sin(vertexPos.z * spineYawLength + (t * spineYawFreq + spineYawCorrectFreq)),0,0) * maskedFactor * 0.3,float4(0,0,0,1));
				vertexPos = mul(rot_mat1, vertexPos);
				return vertexPos;
			}

            float4 SpineRoll(float4 vertexPos, float rollLength, float rollFreq, half maskedFactor, float stopTime, float timeOffset)
			{
            	float t = getTime(stopTime,timeOffset);
				float4x4 rot_mat2;
				rot_mat2 = rotate(float3(0,0,sin(vertexPos.y *rollLength - t * rollFreq)) * 0.55 * maskedFactor ,float4(0,0,0,1));
				vertexPos = mul(rot_mat2, vertexPos);
				return vertexPos;
			}

			float4 SpineTurn(float4 vertexPos, float turnFactorWhenZIsPositive,float turnFactorWhenZIsNegative,float cutOffValueWhenZIsPositive, float cutOffValueWhenZIsNegative)
            {
            	float turnFactor;
            	float cutOffValue;
            	if (vertexPos.z < 0)
            	{
            		cutOffValue = cutOffValueWhenZIsNegative;
            		turnFactor = turnFactorWhenZIsNegative;
            	}else
            	{
            		cutOffValue = cutOffValueWhenZIsPositive;
            		turnFactor = turnFactorWhenZIsPositive;
            	}

            	float distanceToYAxis = length(vertexPos.z);
            	float ratio = distanceToYAxis /cutOffValue;
            	float distanceToZAxis;
            	if (ratio < 1)
            	{
            		distanceToZAxis = turnFactor * ratio * ratio;
            	}else
            	{
            		distanceToZAxis = 2 * turnFactor * ratio - turnFactor;
            	}

            	float rotationAngle = atan(distanceToZAxis / distanceToYAxis);
            	float4x4 rot_mat = rotate(float3(0, rotationAngle, 0), float4(0, 0, 0, 1));
            	vertexPos = mul(rot_mat, vertexPos);

            	// 返回处理后的顶点位置
            	return vertexPos;
            }