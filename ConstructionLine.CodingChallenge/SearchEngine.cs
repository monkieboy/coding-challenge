using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine : ISearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly List<SizeCount> _shirtSizeGroups;
        private readonly List<ColorCount> _shirtColorGroups;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.
            _shirtColorGroups = Color.All.Select(color => new ColorCount { Color = color, Count = shirts.Count(shirt => shirt.Color == color) } ).ToList();
            _shirtSizeGroups = Size.All.Select(size => new SizeCount { Size = size, Count = shirts.Count(shirt => shirt.Size == size) } ).ToList();
        }
        
        public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.
            var filteredShirts = _shirts.Where(shirt => 
                (options.Sizes.Contains(shirt.Size) || !options.Sizes.Any()) 
                && (options.Colors.Contains(shirt.Color) || !options.Colors.Any())
                ).ToList();
            
            var filteredSizes = _shirtSizeGroups.Select(count =>
            {
                count.Count = filteredShirts.Count(x => x.Size == count.Size);
                return count;
            }).ToList();
            
            return new SearchResults
            {
                Shirts = filteredShirts,
                SizeCounts = filteredSizes,
                ColorCounts = _shirtColorGroups
            };
        }
    }
}