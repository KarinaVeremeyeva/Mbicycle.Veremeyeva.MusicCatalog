import { Pipe, PipeTransform } from '@angular/core';
import { Song } from "./_models/song";

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(songs: Song[], searchText: string): Song[] {
    if (!songs || !searchText) {
      return songs;
    }

    return songs.filter((song: Song) => song.name.toLocaleLowerCase().includes(searchText));
  }
}
