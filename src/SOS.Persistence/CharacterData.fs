namespace SOS.Persistence

type CharacterData() =
    member val Name:string                 = ""                  with get, set
    member val CharacterType:CharacterType = CharacterType.Swarm with get, set
    member val X:float                     = 0.0                 with get, set
    member val Y:float                     = 0.0                 with get, set

