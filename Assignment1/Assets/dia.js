 #pragma strict
 
 
public var terrain : Terrain;
public var size : int = 513;
 
var dataArray : float[,];
 
 
function Start() 
{
    DiamondSquareDataArray();
}
 
 
function Update() 
{
    if ( Input.GetKeyDown( KeyCode.Space ) )
        DiamondSquareDataArray();
}
 
 
function DiamondSquareDataArray() 
{
    // declare the data array
    dataArray = new float[ size, size ];
     
    // set the 4 corners
    dataArray[ 0, 0 ] = 1;
    dataArray[ size - 1, 0 ] = 1;
    dataArray[ 0, size - 1 ] = 1;
    dataArray[ size - 1, size - 1 ] = 1;
     
     
    var val : float;
    var rnd : float;
     
    var h : float = 0.5;
     
    var sideLength : int;
    var x : int;
    var y : int;
     
    var halfSide : int;
     
     
    for ( sideLength = size - 1; sideLength >= 2; sideLength /= 2 ) //所处
    {
        halfSide = sideLength / 2;                                  // half
         
        // square values
        for ( x = 0; x < size - 1; x += sideLength )
        {
            for ( y = 0; y < size - 1; y += sideLength )
            {
                val = dataArray[ x, y ];
                val += dataArray[ x + sideLength, y ];
                val += dataArray[ x, y + sideLength ];
                val += dataArray[ x + sideLength, y + sideLength ];
                 
                val /= 4.0;
                 
                // add random
                rnd = ( Random.value * 2.0 * h ) - h;
                val = Mathf.Clamp01( val + rnd );
                 
                dataArray[ x + halfSide, y + halfSide ] = val;
            }
        }
         
        // diamond values
        for ( x = 0; x < size - 1; x += halfSide )      //半格半格前进
        {
            for ( y = ( x + halfSide ) % sideLength; y < size - 1; y += sideLength )    //同样半格半格前进，但是比x大半格（仅当在到中点时）
            {
                val = dataArray[ ( x - halfSide + size - 1 ) % ( size - 1 ), y ];
                val += dataArray[ ( x + halfSide ) % ( size - 1 ), y ];
                val += dataArray[ x, ( y + halfSide ) % ( size - 1 ) ];
                val += dataArray[ x, ( y - halfSide + size - 1 ) % ( size - 1 ) ];
                 
                val /= 4.0;
                 
                // add random
                rnd = ( Random.value * 2.0 * h ) - h;
                val = Mathf.Clamp01( val + rnd );
                 
                dataArray[ x, y ] = val;
                 
                if ( x == 0 ) dataArray[ size - 1, y ] = val;
                if ( y == 0 ) dataArray[ x, size - 1 ] = val;
            }
        }
         
         
        h /= 2.0; // cannot include this in for loop (dont know how in uJS)
    }
     
     
    Debug.Log( "DiamondSquareDataArray completed" );
     
    // Generate Terrain using dataArray as height values
    GenerateTerrain();
}
 
 
function GenerateTerrain() 
{
    if ( !terrain )
        return;
     
    if ( terrain.terrainData.heightmapResolution != size )
     terrain.terrainData.heightmapResolution = size;
     
    terrain.terrainData.SetHeights( 0, 0, dataArray );
}