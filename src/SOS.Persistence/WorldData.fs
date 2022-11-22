namespace SOS.Persistence

open System.Collections.Generic

type public WorldData() =
    member val PlayerCharacterId: int option = None with get, set
    member val Characters: List<CharacterData> = List<CharacterData>() with get, set

