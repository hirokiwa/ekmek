using UnityEngine;

internal sealed class DrawGraphTest : MonoBehaviour
{
    private int m_hp;
    private int m_maxHp = 100;

    private void Update()
    {
        // DrawGraph
        //     .Add( "HP", m_hp )
        //     .SetColor( Color.yellow )
        //     .SetLineWidth( 2 )
        //     .SetGraphHeight( 100 )
        //     .SetStepSize( 0.25f )
        //     .SetLimits( 0, m_maxHp )
        //     ;
        //
        // m_hp = ( m_hp + 1 ) % m_maxHp;
    }
}