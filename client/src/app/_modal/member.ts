import { Photo } from "./photo"

export interface Member {
  id: number
  userName: string
  photoUrl: string
  gender: string
  age: number
  created: Date
  knownAs: string
  lastActive: Date
  introduction: string
  lookingFor: string
  interests: string
  city: string
  country: string
  photos: Photo[]
}

