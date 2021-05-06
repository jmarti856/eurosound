# EuroSound Editor
A C# program where is possible to add music and sounds to Sphinx and the Cursed Mummy game (only for the PC version).

# File types EuroSound creates
In the Sphinx wiki you can see more in detail each file documentation: https://sphinxandthecursedmummy.fandom.com/wiki/SFX here is a little resume:
- `Normal soundbanks:` Every level stores all of the used sound effects in its own sound bank, each sound effect has a series of flags and properties and contains a variable array of raw sound samples.
- `Stream File:` Most of the long, streamed ambient sounds are actually stored here once instead of being duplicated in each soundbank.
- `Music soundbanks`: Each music track is defined via hashcode and stored in its own file. Almost every track has a lead-in time, a middle looping section, and —occasionally— a small ending that is not generally used.
- `SFX_Data.bin: `Contains a special binary array, even if most of that data is redundant; but it probably exists to avoid having to load each soundbank just to get properties like the length of a sound.
