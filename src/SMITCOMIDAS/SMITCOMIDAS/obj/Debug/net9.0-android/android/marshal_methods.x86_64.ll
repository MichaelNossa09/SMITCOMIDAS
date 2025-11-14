; ModuleID = 'marshal_methods.x86_64.ll'
source_filename = "marshal_methods.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [369 x ptr] zeroinitializer, align 16

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [1107 x i64] [
	i64 u0x001e58127c546039, ; 0: lib_System.Globalization.dll.so => 41
	i64 u0x0024d0f62dee05bd, ; 1: Xamarin.KotlinX.Coroutines.Core.dll => 326
	i64 u0x0071cf2d27b7d61e, ; 2: lib_Xamarin.AndroidX.SwipeRefreshLayout.dll.so => 305
	i64 u0x01109b0e4d99e61f, ; 3: System.ComponentModel.Annotations.dll => 13
	i64 u0x02123411c4e01926, ; 4: lib_Xamarin.AndroidX.Navigation.Runtime.dll.so => 294
	i64 u0x0284512fad379f7e, ; 5: System.Runtime.Handles => 104
	i64 u0x02a4c5a44384f885, ; 6: Microsoft.Extensions.Caching.Memory => 192
	i64 u0x02abedc11addc1ed, ; 7: lib_Mono.Android.Runtime.dll.so => 169
	i64 u0x02f55bf70672f5c8, ; 8: lib_System.IO.FileSystem.DriveInfo.dll.so => 47
	i64 u0x032267b2a94db371, ; 9: lib_Xamarin.AndroidX.AppCompat.dll.so => 248
	i64 u0x03621c804933a890, ; 10: System.Buffers => 7
	i64 u0x0399610510a38a38, ; 11: lib_System.Private.DataContractSerialization.dll.so => 85
	i64 u0x043032f1d071fae0, ; 12: ru/Microsoft.Maui.Controls.resources => 354
	i64 u0x044440a55165631e, ; 13: lib-cs-Microsoft.Maui.Controls.resources.dll.so => 332
	i64 u0x046eb1581a80c6b0, ; 14: vi/Microsoft.Maui.Controls.resources => 360
	i64 u0x0470607fd33c32db, ; 15: Microsoft.IdentityModel.Abstractions.dll => 214
	i64 u0x047408741db2431a, ; 16: Xamarin.AndroidX.DynamicAnimation => 268
	i64 u0x0514f1a3ae77a228, ; 17: lib_Xamarin.Kotlin.StdLib.Common.dll.so => 322
	i64 u0x0517ef04e06e9f76, ; 18: System.Net.Primitives => 70
	i64 u0x0565d18c6da3de38, ; 19: Xamarin.AndroidX.RecyclerView => 298
	i64 u0x0581db89237110e9, ; 20: lib_System.Collections.dll.so => 12
	i64 u0x05989cb940b225a9, ; 21: Microsoft.Maui.dll => 223
	i64 u0x05a1c25e78e22d87, ; 22: lib_System.Runtime.CompilerServices.Unsafe.dll.so => 101
	i64 u0x06076b5d2b581f08, ; 23: zh-HK/Microsoft.Maui.Controls.resources => 361
	i64 u0x06388ffe9f6c161a, ; 24: System.Xml.Linq.dll => 154
	i64 u0x06600c4c124cb358, ; 25: System.Configuration.dll => 19
	i64 u0x067f95c5ddab55b3, ; 26: lib_Xamarin.AndroidX.Fragment.Ktx.dll.so => 273
	i64 u0x0680a433c781bb3d, ; 27: Xamarin.AndroidX.Collection.Jvm => 255
	i64 u0x0690533f9fc14683, ; 28: lib_Microsoft.AspNetCore.Components.dll.so => 176
	i64 u0x069fff96ec92a91d, ; 29: System.Xml.XPath.dll => 159
	i64 u0x070b0847e18dab68, ; 30: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 270
	i64 u0x0739448d84d3b016, ; 31: lib_Xamarin.AndroidX.VectorDrawable.dll.so => 308
	i64 u0x07469f2eecce9e85, ; 32: mscorlib.dll => 165
	i64 u0x07c57877c7ba78ad, ; 33: ru/Microsoft.Maui.Controls.resources.dll => 354
	i64 u0x07dcdc7460a0c5e4, ; 34: System.Collections.NonGeneric => 10
	i64 u0x08122e52765333c8, ; 35: lib_Microsoft.Extensions.Logging.Debug.dll.so => 209
	i64 u0x088610fc2509f69e, ; 36: lib_Xamarin.AndroidX.VectorDrawable.Animated.dll.so => 309
	i64 u0x08881a0a9768df86, ; 37: lib_Azure.Core.dll.so => 172
	i64 u0x08a7c865576bbde7, ; 38: System.Reflection.Primitives => 95
	i64 u0x08c9d051a4a817e5, ; 39: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 265
	i64 u0x08f3c9788ee2153c, ; 40: Xamarin.AndroidX.DrawerLayout => 267
	i64 u0x09138715c92dba90, ; 41: lib_System.ComponentModel.Annotations.dll.so => 13
	i64 u0x0919c28b89381a0b, ; 42: lib_Microsoft.Extensions.Options.dll.so => 210
	i64 u0x092266563089ae3e, ; 43: lib_System.Collections.NonGeneric.dll.so => 10
	i64 u0x095cacaf6b6a32e4, ; 44: System.Memory.Data => 233
	i64 u0x09d144a7e214d457, ; 45: System.Security.Cryptography => 126
	i64 u0x09e2b9f743db21a8, ; 46: lib_System.Reflection.Metadata.dll.so => 94
	i64 u0x0a805f95d98f597b, ; 47: lib_Microsoft.Extensions.Caching.Abstractions.dll.so => 191
	i64 u0x0abb3e2b271edc45, ; 48: System.Threading.Channels.dll => 138
	i64 u0x0adeb6c0f5699d33, ; 49: Microsoft.Data.SqlClient.dll => 186
	i64 u0x0b06b1feab070143, ; 50: System.Formats.Tar => 38
	i64 u0x0b3b632c3bbee20c, ; 51: sk/Microsoft.Maui.Controls.resources => 355
	i64 u0x0b6aff547b84fbe9, ; 52: Xamarin.KotlinX.Serialization.Core.Jvm => 329
	i64 u0x0be1e582d0d8ef1a, ; 53: lib_Microsoft.AspNetCore.Cryptography.KeyDerivation.dll.so => 182
	i64 u0x0be2e1f8ce4064ed, ; 54: Xamarin.AndroidX.ViewPager => 311
	i64 u0x0c3ca6cc978e2aae, ; 55: pt-BR/Microsoft.Maui.Controls.resources => 351
	i64 u0x0c59ad9fbbd43abe, ; 56: Mono.Android => 170
	i64 u0x0c65741e86371ee3, ; 57: lib_Xamarin.Android.Glide.GifDecoder.dll.so => 242
	i64 u0x0c74af560004e816, ; 58: Microsoft.Win32.Registry.dll => 5
	i64 u0x0c7790f60165fc06, ; 59: lib_Microsoft.Maui.Essentials.dll.so => 224
	i64 u0x0c83c82812e96127, ; 60: lib_System.Net.Mail.dll.so => 66
	i64 u0x0cce4bce83380b7f, ; 61: Xamarin.AndroidX.Security.SecurityCrypto => 302
	i64 u0x0d13cd7cce4284e4, ; 62: System.Security.SecureString => 129
	i64 u0x0d34fb076d8103ae, ; 63: Microsoft.Extensions.Identity.Core.dll => 205
	i64 u0x0d3b5ab8b2766190, ; 64: lib_Microsoft.Bcl.AsyncInterfaces.dll.so => 185
	i64 u0x0d63f4f73521c24f, ; 65: lib_Xamarin.AndroidX.SavedState.SavedState.Ktx.dll.so => 301
	i64 u0x0e04e702012f8463, ; 66: Xamarin.AndroidX.Emoji2 => 269
	i64 u0x0e14e73a54dda68e, ; 67: lib_System.Net.NameResolution.dll.so => 67
	i64 u0x0f37dd7a62ae99af, ; 68: lib_Xamarin.AndroidX.Collection.Ktx.dll.so => 256
	i64 u0x0f5e7abaa7cf470a, ; 69: System.Net.HttpListener => 65
	i64 u0x1001f97bbe242e64, ; 70: System.IO.UnmanagedMemoryStream => 56
	i64 u0x102861e4055f511a, ; 71: Microsoft.Bcl.AsyncInterfaces.dll => 185
	i64 u0x102a31b45304b1da, ; 72: Xamarin.AndroidX.CustomView => 264
	i64 u0x1065c4cb554c3d75, ; 73: System.IO.IsolatedStorage.dll => 51
	i64 u0x10f6cfcbcf801616, ; 74: System.IO.Compression.Brotli => 42
	i64 u0x114443cdcf2091f1, ; 75: System.Security.Cryptography.Primitives => 124
	i64 u0x11a603952763e1d4, ; 76: System.Net.Mail => 66
	i64 u0x11a70d0e1009fb11, ; 77: System.Net.WebSockets.dll => 80
	i64 u0x11f26371eee0d3c1, ; 78: lib_Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll.so => 284
	i64 u0x11fbe62d469cc1c8, ; 79: Microsoft.VisualStudio.DesignTools.TapContract.dll => 366
	i64 u0x12128b3f59302d47, ; 80: lib_System.Xml.Serialization.dll.so => 156
	i64 u0x123639456fb056da, ; 81: System.Reflection.Emit.Lightweight.dll => 91
	i64 u0x12521e9764603eaa, ; 82: lib_System.Resources.Reader.dll.so => 98
	i64 u0x125b7f94acb989db, ; 83: Xamarin.AndroidX.RecyclerView.dll => 298
	i64 u0x126ee4b0de53cbfd, ; 84: Microsoft.IdentityModel.Protocols.OpenIdConnect.dll => 218
	i64 u0x12d3b63863d4ab0b, ; 85: lib_System.Threading.Overlapped.dll.so => 139
	i64 u0x134eab1061c395ee, ; 86: System.Transactions => 149
	i64 u0x137b34d6751da129, ; 87: System.Drawing.Common => 230
	i64 u0x138567fa954faa55, ; 88: Xamarin.AndroidX.Browser => 252
	i64 u0x13a01de0cbc3f06c, ; 89: lib-fr-Microsoft.Maui.Controls.resources.dll.so => 338
	i64 u0x13beedefb0e28a45, ; 90: lib_System.Xml.XmlDocument.dll.so => 160
	i64 u0x13f1e5e209e91af4, ; 91: lib_Java.Interop.dll.so => 167
	i64 u0x13f1e880c25d96d1, ; 92: he/Microsoft.Maui.Controls.resources => 339
	i64 u0x143a1f6e62b82b56, ; 93: Microsoft.IdentityModel.Protocols.OpenIdConnect => 218
	i64 u0x143d8ea60a6a4011, ; 94: Microsoft.Extensions.DependencyInjection.Abstractions => 199
	i64 u0x1497051b917530bd, ; 95: lib_System.Net.WebSockets.dll.so => 80
	i64 u0x14b78ce3adce0011, ; 96: Microsoft.VisualStudio.DesignTools.TapContract => 366
	i64 u0x14e68447938213b7, ; 97: Xamarin.AndroidX.Collection.Ktx.dll => 256
	i64 u0x152a448bd1e745a7, ; 98: Microsoft.Win32.Primitives => 4
	i64 u0x1557de0138c445f4, ; 99: lib_Microsoft.Win32.Registry.dll.so => 5
	i64 u0x15bdc156ed462f2f, ; 100: lib_System.IO.FileSystem.dll.so => 50
	i64 u0x15e300c2c1668655, ; 101: System.Resources.Writer.dll => 100
	i64 u0x16bf2a22df043a09, ; 102: System.IO.Pipes.dll => 55
	i64 u0x16ea2b318ad2d830, ; 103: System.Security.Cryptography.Algorithms => 119
	i64 u0x16eeae54c7ebcc08, ; 104: System.Reflection.dll => 97
	i64 u0x17125c9a85b4929f, ; 105: lib_netstandard.dll.so => 166
	i64 u0x1716866f7416792e, ; 106: lib_System.Security.AccessControl.dll.so => 117
	i64 u0x174f71c46216e44a, ; 107: Xamarin.KotlinX.Coroutines.Core => 326
	i64 u0x1752c12f1e1fc00c, ; 108: System.Core => 21
	i64 u0x17b56e25558a5d36, ; 109: lib-hu-Microsoft.Maui.Controls.resources.dll.so => 342
	i64 u0x17f9358913beb16a, ; 110: System.Text.Encodings.Web => 136
	i64 u0x1809fb23f29ba44a, ; 111: lib_System.Reflection.TypeExtensions.dll.so => 96
	i64 u0x18402a709e357f3b, ; 112: lib_Xamarin.KotlinX.Serialization.Core.Jvm.dll.so => 329
	i64 u0x18a9befae51bb361, ; 113: System.Net.WebClient => 76
	i64 u0x18f0ce884e87d89a, ; 114: nb/Microsoft.Maui.Controls.resources.dll => 348
	i64 u0x193d7a04b7eda8bc, ; 115: lib_Xamarin.AndroidX.Print.dll.so => 296
	i64 u0x19777fba3c41b398, ; 116: Xamarin.AndroidX.Startup.StartupRuntime.dll => 304
	i64 u0x19a4c090f14ebb66, ; 117: System.Security.Claims => 118
	i64 u0x1a6fceea64859810, ; 118: Azure.Identity => 173
	i64 u0x1a91866a319e9259, ; 119: lib_System.Collections.Concurrent.dll.so => 8
	i64 u0x1aac34d1917ba5d3, ; 120: lib_System.dll.so => 163
	i64 u0x1aad60783ffa3e5b, ; 121: lib-th-Microsoft.Maui.Controls.resources.dll.so => 357
	i64 u0x1aea8f1c3b282172, ; 122: lib_System.Net.Ping.dll.so => 69
	i64 u0x1b4b7a1d0d265fa2, ; 123: Xamarin.Android.Glide.DiskLruCache => 241
	i64 u0x1b8700ce6e547c0b, ; 124: lib_Microsoft.AspNetCore.Components.Forms.dll.so => 177
	i64 u0x1bbdb16cfa73e785, ; 125: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android => 285
	i64 u0x1bc766e07b2b4241, ; 126: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 299
	i64 u0x1c5217a9e4973753, ; 127: lib_Microsoft.Extensions.FileProviders.Physical.dll.so => 203
	i64 u0x1c753b5ff15bce1b, ; 128: Mono.Android.Runtime.dll => 169
	i64 u0x1cd47467799d8250, ; 129: System.Threading.Tasks.dll => 143
	i64 u0x1d23eafdc6dc346c, ; 130: System.Globalization.Calendars.dll => 39
	i64 u0x1da4110562816681, ; 131: Xamarin.AndroidX.Security.SecurityCrypto.dll => 302
	i64 u0x1db6820994506bf5, ; 132: System.IO.FileSystem.AccessControl.dll => 46
	i64 u0x1dbb0c2c6a999acb, ; 133: System.Diagnostics.StackTrace => 30
	i64 u0x1e3d87657e9659bc, ; 134: Xamarin.AndroidX.Navigation.UI => 295
	i64 u0x1e71143913d56c10, ; 135: lib-ko-Microsoft.Maui.Controls.resources.dll.so => 346
	i64 u0x1e7c31185e2fb266, ; 136: lib_System.Threading.Tasks.Parallel.dll.so => 142
	i64 u0x1ed8fcce5e9b50a0, ; 137: Microsoft.Extensions.Options.dll => 210
	i64 u0x1f055d15d807e1b2, ; 138: System.Xml.XmlSerializer => 161
	i64 u0x1f198ea93d5594b5, ; 139: Microsoft.Extensions.Identity.Core => 205
	i64 u0x1f1ed22c1085f044, ; 140: lib_System.Diagnostics.FileVersionInfo.dll.so => 28
	i64 u0x1f61df9c5b94d2c1, ; 141: lib_System.Numerics.dll.so => 83
	i64 u0x1f750bb5421397de, ; 142: lib_Xamarin.AndroidX.Tracing.Tracing.dll.so => 306
	i64 u0x20237ea48006d7a8, ; 143: lib_System.Net.WebClient.dll.so => 76
	i64 u0x209375905fcc1bad, ; 144: lib_System.IO.Compression.Brotli.dll.so => 42
	i64 u0x20edad43b59fbd8e, ; 145: System.Security.Permissions.dll => 236
	i64 u0x20fab3cf2dfbc8df, ; 146: lib_System.Diagnostics.Process.dll.so => 29
	i64 u0x2110167c128cba15, ; 147: System.Globalization => 41
	i64 u0x21419508838f7547, ; 148: System.Runtime.CompilerServices.VisualC => 102
	i64 u0x2174319c0d835bc9, ; 149: System.Runtime => 116
	i64 u0x2198e5bc8b7153fa, ; 150: Xamarin.AndroidX.Annotation.Experimental.dll => 246
	i64 u0x2199f06354c82d3b, ; 151: System.ClientModel.dll => 228
	i64 u0x219ea1b751a4dee4, ; 152: lib_System.IO.Compression.ZipFile.dll.so => 44
	i64 u0x21cc7e445dcd5469, ; 153: System.Reflection.Emit.ILGeneration => 90
	i64 u0x220fd4f2e7c48170, ; 154: th/Microsoft.Maui.Controls.resources => 357
	i64 u0x224538d85ed15a82, ; 155: System.IO.Pipes => 55
	i64 u0x22908438c6bed1af, ; 156: lib_System.Threading.Timer.dll.so => 146
	i64 u0x22fbc14e981e3b45, ; 157: lib_Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll.so => 365
	i64 u0x237be844f1f812c7, ; 158: System.Threading.Thread.dll => 144
	i64 u0x23807c59646ec4f3, ; 159: lib_Microsoft.EntityFrameworkCore.dll.so => 187
	i64 u0x23852b3bdc9f7096, ; 160: System.Resources.ResourceManager => 99
	i64 u0x23986dd7e5d4fc01, ; 161: System.IO.FileSystem.Primitives.dll => 48
	i64 u0x2407aef2bbe8fadf, ; 162: System.Console => 20
	i64 u0x240abe014b27e7d3, ; 163: Xamarin.AndroidX.Core.dll => 261
	i64 u0x247619fe4413f8bf, ; 164: System.Runtime.Serialization.Primitives.dll => 113
	i64 u0x24de8d301281575e, ; 165: Xamarin.Android.Glide => 239
	i64 u0x252073cc3caa62c2, ; 166: fr/Microsoft.Maui.Controls.resources.dll => 338
	i64 u0x256b8d41255f01b1, ; 167: Xamarin.Google.Crypto.Tink.Android => 317
	i64 u0x261688487d5bb602, ; 168: lib_SMITCOMIDAS.Shared.dll.so => 367
	i64 u0x2662c629b96b0b30, ; 169: lib_Xamarin.Kotlin.StdLib.dll.so => 321
	i64 u0x268c1439f13bcc29, ; 170: lib_Microsoft.Extensions.Primitives.dll.so => 211
	i64 u0x26a670e154a9c54b, ; 171: System.Reflection.Extensions.dll => 93
	i64 u0x26d077d9678fe34f, ; 172: System.IO.dll => 57
	i64 u0x270a44600c921861, ; 173: System.IdentityModel.Tokens.Jwt => 232
	i64 u0x273f3515de5faf0d, ; 174: id/Microsoft.Maui.Controls.resources.dll => 343
	i64 u0x2742545f9094896d, ; 175: hr/Microsoft.Maui.Controls.resources => 341
	i64 u0x2759af78ab94d39b, ; 176: System.Net.WebSockets => 80
	i64 u0x27b2b16f3e9de038, ; 177: Xamarin.Google.Crypto.Tink.Android.dll => 317
	i64 u0x27b410442fad6cf1, ; 178: Java.Interop.dll => 167
	i64 u0x27b97e0d52c3034a, ; 179: System.Diagnostics.Debug => 26
	i64 u0x2801845a2c71fbfb, ; 180: System.Net.Primitives.dll => 70
	i64 u0x286835e259162700, ; 181: lib_Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll.so => 297
	i64 u0x2949f3617a02c6b2, ; 182: Xamarin.AndroidX.ExifInterface => 271
	i64 u0x29a6504669341b9f, ; 183: SMITCOMIDAS.dll => 0
	i64 u0x29f947844fb7fc11, ; 184: Microsoft.Maui.Controls.HotReload.Forms => 364
	i64 u0x2a128783efe70ba0, ; 185: uk/Microsoft.Maui.Controls.resources.dll => 359
	i64 u0x2a3b095612184159, ; 186: lib_System.Net.NetworkInformation.dll.so => 68
	i64 u0x2a6507a5ffabdf28, ; 187: System.Diagnostics.TraceSource.dll => 33
	i64 u0x2ac82b8d1ecafc7c, ; 188: lib_System.Windows.Extensions.dll.so => 238
	i64 u0x2ad156c8e1354139, ; 189: fi/Microsoft.Maui.Controls.resources => 337
	i64 u0x2ad5d6b13b7a3e04, ; 190: System.ComponentModel.DataAnnotations.dll => 14
	i64 u0x2af298f63581d886, ; 191: System.Text.RegularExpressions.dll => 137
	i64 u0x2af615542f04da50, ; 192: System.IdentityModel.Tokens.Jwt.dll => 232
	i64 u0x2afc1c4f898552ee, ; 193: lib_System.Formats.Asn1.dll.so => 231
	i64 u0x2b148910ed40fbf9, ; 194: zh-Hant/Microsoft.Maui.Controls.resources.dll => 363
	i64 u0x2b4d4904cebfa4e9, ; 195: Microsoft.Extensions.FileSystemGlobbing => 204
	i64 u0x2b6989d78cba9a15, ; 196: Xamarin.AndroidX.Concurrent.Futures.dll => 257
	i64 u0x2c8bd14bb93a7d82, ; 197: lib-pl-Microsoft.Maui.Controls.resources.dll.so => 350
	i64 u0x2cbd9262ca785540, ; 198: lib_System.Text.Encoding.CodePages.dll.so => 133
	i64 u0x2cc9e1fed6257257, ; 199: lib_System.Reflection.Emit.Lightweight.dll.so => 91
	i64 u0x2cd723e9fe623c7c, ; 200: lib_System.Private.Xml.Linq.dll.so => 87
	i64 u0x2d169d318a968379, ; 201: System.Threading.dll => 147
	i64 u0x2d47774b7d993f59, ; 202: sv/Microsoft.Maui.Controls.resources.dll => 356
	i64 u0x2d5ffcae1ad0aaca, ; 203: System.Data.dll => 24
	i64 u0x2db915caf23548d2, ; 204: System.Text.Json.dll => 237
	i64 u0x2dcaa0bb15a4117a, ; 205: System.IO.UnmanagedMemoryStream.dll => 56
	i64 u0x2e5a40c319acb800, ; 206: System.IO.FileSystem => 50
	i64 u0x2e6f1f226821322a, ; 207: el/Microsoft.Maui.Controls.resources.dll => 335
	i64 u0x2e8ff3fae87a8245, ; 208: lib_Microsoft.JSInterop.dll.so => 220
	i64 u0x2f02f94df3200fe5, ; 209: System.Diagnostics.Process => 29
	i64 u0x2f2e98e1c89b1aff, ; 210: System.Xml.ReaderWriter => 155
	i64 u0x2f40b2521deba305, ; 211: lib_Microsoft.SqlServer.Server.dll.so => 226
	i64 u0x2f5911d9ba814e4e, ; 212: System.Diagnostics.Tracing => 34
	i64 u0x2f84070a459bc31f, ; 213: lib_System.Xml.dll.so => 162
	i64 u0x2feb4d2fcda05cfd, ; 214: Microsoft.Extensions.Caching.Abstractions.dll => 191
	i64 u0x309ee9eeec09a71e, ; 215: lib_Xamarin.AndroidX.Fragment.dll.so => 272
	i64 u0x309f2bedefa9a318, ; 216: Microsoft.IdentityModel.Abstractions => 214
	i64 u0x30c6dda129408828, ; 217: System.IO.IsolatedStorage => 51
	i64 u0x310d9651ec86c411, ; 218: Microsoft.Extensions.FileProviders.Embedded => 202
	i64 u0x31195fef5d8fb552, ; 219: _Microsoft.Android.Resource.Designer.dll => 368
	i64 u0x312c8ed623cbfc8d, ; 220: Xamarin.AndroidX.Window.dll => 313
	i64 u0x31496b779ed0663d, ; 221: lib_System.Reflection.DispatchProxy.dll.so => 89
	i64 u0x32243413e774362a, ; 222: Xamarin.AndroidX.CardView.dll => 253
	i64 u0x3235427f8d12dae1, ; 223: lib_System.Drawing.Primitives.dll.so => 35
	i64 u0x329753a17a517811, ; 224: fr/Microsoft.Maui.Controls.resources => 338
	i64 u0x32aa989ff07a84ff, ; 225: lib_System.Xml.ReaderWriter.dll.so => 155
	i64 u0x33642d5508314e46, ; 226: Microsoft.Extensions.FileSystemGlobbing.dll => 204
	i64 u0x33829542f112d59b, ; 227: System.Collections.Immutable => 9
	i64 u0x33a31443733849fe, ; 228: lib-es-Microsoft.Maui.Controls.resources.dll.so => 336
	i64 u0x341abc357fbb4ebf, ; 229: lib_System.Net.Sockets.dll.so => 75
	i64 u0x348d598f4054415e, ; 230: Microsoft.SqlServer.Server => 226
	i64 u0x3496c1e2dcaf5ecc, ; 231: lib_System.IO.Pipes.AccessControl.dll.so => 54
	i64 u0x34bd01fd4be06ee3, ; 232: lib_Microsoft.Extensions.FileProviders.Composite.dll.so => 201
	i64 u0x34dfd74fe2afcf37, ; 233: Microsoft.Maui => 223
	i64 u0x34e292762d9615df, ; 234: cs/Microsoft.Maui.Controls.resources.dll => 332
	i64 u0x3508234247f48404, ; 235: Microsoft.Maui.Controls => 221
	i64 u0x353590da528c9d22, ; 236: System.ComponentModel.Annotations => 13
	i64 u0x3549870798b4cd30, ; 237: lib_Xamarin.AndroidX.ViewPager2.dll.so => 312
	i64 u0x355282fc1c909694, ; 238: Microsoft.Extensions.Configuration => 193
	i64 u0x3552fc5d578f0fbf, ; 239: Xamarin.AndroidX.Arch.Core.Common => 250
	i64 u0x355c649948d55d97, ; 240: lib_System.Runtime.Intrinsics.dll.so => 108
	i64 u0x35ea9d1c6834bc8c, ; 241: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 288
	i64 u0x3628ab68db23a01a, ; 242: lib_System.Diagnostics.Tools.dll.so => 32
	i64 u0x3673b042508f5b6b, ; 243: lib_System.Runtime.Extensions.dll.so => 103
	i64 u0x36740f1a8ecdc6c4, ; 244: System.Numerics => 83
	i64 u0x36b2b50fdf589ae2, ; 245: System.Reflection.Emit.Lightweight => 91
	i64 u0x36cada77dc79928b, ; 246: System.IO.MemoryMappedFiles => 52
	i64 u0x374ef46b06791af6, ; 247: System.Reflection.Primitives.dll => 95
	i64 u0x376bf93e521a5417, ; 248: lib_Xamarin.Jetbrains.Annotations.dll.so => 320
	i64 u0x37bc29f3183003b6, ; 249: lib_System.IO.dll.so => 57
	i64 u0x37fd73cba07e0b9d, ; 250: lib_Microsoft.AspNetCore.Cryptography.Internal.dll.so => 181
	i64 u0x380134e03b1e160a, ; 251: System.Collections.Immutable.dll => 9
	i64 u0x38049b5c59b39324, ; 252: System.Runtime.CompilerServices.Unsafe => 101
	i64 u0x385c17636bb6fe6e, ; 253: Xamarin.AndroidX.CustomView.dll => 264
	i64 u0x38869c811d74050e, ; 254: System.Net.NameResolution.dll => 67
	i64 u0x38e93ec1c057cdf6, ; 255: Microsoft.IdentityModel.Protocols => 217
	i64 u0x39251dccb84bdcaa, ; 256: lib_System.Configuration.ConfigurationManager.dll.so => 229
	i64 u0x393c226616977fdb, ; 257: lib_Xamarin.AndroidX.ViewPager.dll.so => 311
	i64 u0x395e37c3334cf82a, ; 258: lib-ca-Microsoft.Maui.Controls.resources.dll.so => 331
	i64 u0x39c3107c28752af1, ; 259: lib_Microsoft.Extensions.FileProviders.Abstractions.dll.so => 200
	i64 u0x3ab5859054645f72, ; 260: System.Security.Cryptography.Primitives.dll => 124
	i64 u0x3ad75090c3fac0e9, ; 261: lib_Xamarin.AndroidX.ResourceInspection.Annotation.dll.so => 299
	i64 u0x3ae44ac43a1fbdbb, ; 262: System.Runtime.Serialization => 115
	i64 u0x3b860f9932505633, ; 263: lib_System.Text.Encoding.Extensions.dll.so => 134
	i64 u0x3be6248c2bc7dc8c, ; 264: Microsoft.JSInterop.dll => 220
	i64 u0x3bea9ebe8c027c01, ; 265: lib_Microsoft.IdentityModel.Tokens.dll.so => 219
	i64 u0x3c3aafb6b3a00bf6, ; 266: lib_System.Security.Cryptography.X509Certificates.dll.so => 125
	i64 u0x3c4049146b59aa90, ; 267: System.Runtime.InteropServices.JavaScript => 105
	i64 u0x3c5f19e4acdcebd8, ; 268: lib_Microsoft.Data.SqlClient.dll.so => 186
	i64 u0x3c7c495f58ac5ee9, ; 269: Xamarin.Kotlin.StdLib => 321
	i64 u0x3c7e5ed3d5db71bb, ; 270: System.Security => 130
	i64 u0x3cd9d281d402eb9b, ; 271: Xamarin.AndroidX.Browser.dll => 252
	i64 u0x3d1c50cc001a991e, ; 272: Xamarin.Google.Guava.ListenableFuture.dll => 319
	i64 u0x3d2b1913edfc08d7, ; 273: lib_System.Threading.ThreadPool.dll.so => 145
	i64 u0x3d46f0b995082740, ; 274: System.Xml.Linq => 154
	i64 u0x3d8a8f400514a790, ; 275: Xamarin.AndroidX.Fragment.Ktx.dll => 273
	i64 u0x3d9c2a242b040a50, ; 276: lib_Xamarin.AndroidX.Core.dll.so => 261
	i64 u0x3db495de2204755c, ; 277: Microsoft.Extensions.Configuration.FileExtensions => 196
	i64 u0x3dbb6b9f5ab90fa7, ; 278: lib_Xamarin.AndroidX.DynamicAnimation.dll.so => 268
	i64 u0x3e5441657549b213, ; 279: Xamarin.AndroidX.ResourceInspection.Annotation => 299
	i64 u0x3e57d4d195c53c2e, ; 280: System.Reflection.TypeExtensions => 96
	i64 u0x3e616ab4ed1f3f15, ; 281: lib_System.Data.dll.so => 24
	i64 u0x3e7f8912b96e5065, ; 282: Microsoft.AspNetCore.Components.WebView.dll => 179
	i64 u0x3f1d226e6e06db7e, ; 283: Xamarin.AndroidX.SlidingPaneLayout.dll => 303
	i64 u0x3f3c8f45ab6f28c7, ; 284: Microsoft.Identity.Client.Extensions.Msal.dll => 213
	i64 u0x3f510adf788828dd, ; 285: System.Threading.Tasks.Extensions => 141
	i64 u0x407740ff2e914d86, ; 286: Xamarin.AndroidX.Print.dll => 296
	i64 u0x407a10bb4bf95829, ; 287: lib_Xamarin.AndroidX.Navigation.Common.dll.so => 292
	i64 u0x407ac43dee26bd5a, ; 288: lib_Azure.Identity.dll.so => 173
	i64 u0x40c98b6bd77346d4, ; 289: Microsoft.VisualBasic.dll => 3
	i64 u0x415e36f6b13ff6f3, ; 290: System.Configuration.ConfigurationManager.dll => 229
	i64 u0x41833cf766d27d96, ; 291: mscorlib => 165
	i64 u0x41cab042be111c34, ; 292: lib_Xamarin.AndroidX.AppCompat.AppCompatResources.dll.so => 249
	i64 u0x423a9ecc4d905a88, ; 293: lib_System.Resources.ResourceManager.dll.so => 99
	i64 u0x423bf51ae7def810, ; 294: System.Xml.XPath => 159
	i64 u0x42462ff15ddba223, ; 295: System.Resources.Reader.dll => 98
	i64 u0x42a31b86e6ccc3f0, ; 296: System.Diagnostics.Contracts => 25
	i64 u0x430e95b891249788, ; 297: lib_System.Reflection.Emit.dll.so => 92
	i64 u0x43375950ec7c1b6a, ; 298: netstandard.dll => 166
	i64 u0x434c4e1d9284cdae, ; 299: Mono.Android.dll => 170
	i64 u0x43505013578652a0, ; 300: lib_Xamarin.AndroidX.Activity.Ktx.dll.so => 244
	i64 u0x437d06c381ed575a, ; 301: lib_Microsoft.VisualBasic.dll.so => 3
	i64 u0x43950f84de7cc79a, ; 302: pl/Microsoft.Maui.Controls.resources.dll => 350
	i64 u0x43e8ca5bc927ff37, ; 303: lib_Xamarin.AndroidX.Emoji2.ViewsHelper.dll.so => 270
	i64 u0x448bd33429269b19, ; 304: Microsoft.CSharp => 1
	i64 u0x4499fa3c8e494654, ; 305: lib_System.Runtime.Serialization.Primitives.dll.so => 113
	i64 u0x4515080865a951a5, ; 306: Xamarin.Kotlin.StdLib.dll => 321
	i64 u0x453c1277f85cf368, ; 307: lib_Microsoft.EntityFrameworkCore.Abstractions.dll.so => 188
	i64 u0x4545802489b736b9, ; 308: Xamarin.AndroidX.Fragment.Ktx => 273
	i64 u0x454b4d1e66bb783c, ; 309: Xamarin.AndroidX.Lifecycle.Process => 281
	i64 u0x458d2df79ac57c1d, ; 310: lib_System.IdentityModel.Tokens.Jwt.dll.so => 232
	i64 u0x45c40276a42e283e, ; 311: System.Diagnostics.TraceSource => 33
	i64 u0x45d443f2a29adc37, ; 312: System.AppContext.dll => 6
	i64 u0x46a4213bc97fe5ae, ; 313: lib-ru-Microsoft.Maui.Controls.resources.dll.so => 354
	i64 u0x47358bd471172e1d, ; 314: lib_System.Xml.Linq.dll.so => 154
	i64 u0x4787a936949fcac2, ; 315: System.Memory.Data.dll => 233
	i64 u0x47daf4e1afbada10, ; 316: pt/Microsoft.Maui.Controls.resources => 352
	i64 u0x480c0a47dd42dd81, ; 317: lib_System.IO.MemoryMappedFiles.dll.so => 52
	i64 u0x488d293220a4fe37, ; 318: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 275
	i64 u0x4953c088b9debf0a, ; 319: lib_System.Security.Permissions.dll.so => 236
	i64 u0x49e952f19a4e2022, ; 320: System.ObjectModel => 84
	i64 u0x49f9e6948a8131e4, ; 321: lib_Xamarin.AndroidX.VersionedParcelable.dll.so => 310
	i64 u0x4a5667b2462a664b, ; 322: lib_Xamarin.AndroidX.Navigation.UI.dll.so => 295
	i64 u0x4a7a18981dbd56bc, ; 323: System.IO.Compression.FileSystem.dll => 43
	i64 u0x4aa5c60350917c06, ; 324: lib_Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll.so => 280
	i64 u0x4b07a0ed0ab33ff4, ; 325: System.Runtime.Extensions.dll => 103
	i64 u0x4b576d47ac054f3c, ; 326: System.IO.FileSystem.AccessControl => 46
	i64 u0x4b7b6532ded934b7, ; 327: System.Text.Json => 237
	i64 u0x4b8f8ea3c2df6bb0, ; 328: System.ClientModel => 228
	i64 u0x4c7755cf07ad2d5f, ; 329: System.Net.Http.Json.dll => 63
	i64 u0x4ca014ceac582c86, ; 330: Microsoft.EntityFrameworkCore.Relational.dll => 189
	i64 u0x4cc5f15266470798, ; 331: lib_Xamarin.AndroidX.Loader.dll.so => 290
	i64 u0x4cf6f67dc77aacd2, ; 332: System.Net.NetworkInformation.dll => 68
	i64 u0x4d3183dd245425d4, ; 333: System.Net.WebSockets.Client.dll => 79
	i64 u0x4d479f968a05e504, ; 334: System.Linq.Expressions.dll => 58
	i64 u0x4d55a010ffc4faff, ; 335: System.Private.Xml => 88
	i64 u0x4d5cbe77561c5b2e, ; 336: System.Web.dll => 152
	i64 u0x4d6001db23f8cd87, ; 337: lib_System.ClientModel.dll.so => 228
	i64 u0x4d77512dbd86ee4c, ; 338: lib_Xamarin.AndroidX.Arch.Core.Common.dll.so => 250
	i64 u0x4d7793536e79c309, ; 339: System.ServiceProcess => 132
	i64 u0x4d95fccc1f67c7ca, ; 340: System.Runtime.Loader.dll => 109
	i64 u0x4dcf44c3c9b076a2, ; 341: it/Microsoft.Maui.Controls.resources.dll => 344
	i64 u0x4dd9247f1d2c3235, ; 342: Xamarin.AndroidX.Loader.dll => 290
	i64 u0x4df510084e2a0bae, ; 343: Microsoft.JSInterop => 220
	i64 u0x4e2aeee78e2c4a87, ; 344: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 297
	i64 u0x4e32f00cb0937401, ; 345: Mono.Android.Runtime => 169
	i64 u0x4e3369190c3dcd08, ; 346: Microsoft.Extensions.Identity.Stores => 206
	i64 u0x4e5eea4668ac2b18, ; 347: System.Text.Encoding.CodePages => 133
	i64 u0x4ebd0c4b82c5eefc, ; 348: lib_System.Threading.Channels.dll.so => 138
	i64 u0x4ee8eaa9c9c1151a, ; 349: System.Globalization.Calendars => 39
	i64 u0x4f21ee6ef9eb527e, ; 350: ca/Microsoft.Maui.Controls.resources => 331
	i64 u0x4fdc964ec1888e25, ; 351: lib_Microsoft.Extensions.Configuration.Binder.dll.so => 195
	i64 u0x4ffd65baff757598, ; 352: Microsoft.IdentityModel.Tokens => 219
	i64 u0x5037f0be3c28c7a3, ; 353: lib_Microsoft.Maui.Controls.dll.so => 221
	i64 u0x50c3a29b21050d45, ; 354: System.Linq.Parallel.dll => 59
	i64 u0x5116b21580ae6eb0, ; 355: Microsoft.Extensions.Configuration.Binder.dll => 195
	i64 u0x5131bbe80989093f, ; 356: Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll => 287
	i64 u0x516324a5050a7e3c, ; 357: System.Net.WebProxy => 78
	i64 u0x516d6f0b21a303de, ; 358: lib_System.Diagnostics.Contracts.dll.so => 25
	i64 u0x51bb8a2afe774e32, ; 359: System.Drawing => 36
	i64 u0x5247c5c32a4140f0, ; 360: System.Resources.Reader => 98
	i64 u0x526bb15e3c386364, ; 361: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 284
	i64 u0x526ce79eb8e90527, ; 362: lib_System.Net.Primitives.dll.so => 70
	i64 u0x52829f00b4467c38, ; 363: lib_System.Data.Common.dll.so => 22
	i64 u0x529ffe06f39ab8db, ; 364: Xamarin.AndroidX.Core => 261
	i64 u0x52ff996554dbf352, ; 365: Microsoft.Maui.Graphics => 225
	i64 u0x5324b9a9dedb24aa, ; 366: Microsoft.AspNetCore.Cryptography.Internal => 181
	i64 u0x535f7e40e8fef8af, ; 367: lib-sk-Microsoft.Maui.Controls.resources.dll.so => 355
	i64 u0x53978aac584c666e, ; 368: lib_System.Security.Cryptography.Cng.dll.so => 120
	i64 u0x53a96d5c86c9e194, ; 369: System.Net.NetworkInformation => 68
	i64 u0x53be1038a61e8d44, ; 370: System.Runtime.InteropServices.RuntimeInformation.dll => 106
	i64 u0x53c3014b9437e684, ; 371: lib-zh-HK-Microsoft.Maui.Controls.resources.dll.so => 361
	i64 u0x53e450ebd586f842, ; 372: lib_Xamarin.AndroidX.LocalBroadcastManager.dll.so => 291
	i64 u0x5435e6f049e9bc37, ; 373: System.Security.Claims.dll => 118
	i64 u0x54795225dd1587af, ; 374: lib_System.Runtime.dll.so => 116
	i64 u0x547a34f14e5f6210, ; 375: Xamarin.AndroidX.Lifecycle.Common.dll => 276
	i64 u0x556e8b63b660ab8b, ; 376: Xamarin.AndroidX.Lifecycle.Common.Jvm.dll => 277
	i64 u0x5588627c9a108ec9, ; 377: System.Collections.Specialized => 11
	i64 u0x55a898e4f42e3fae, ; 378: Microsoft.VisualBasic.Core.dll => 2
	i64 u0x55fa0c610fe93bb1, ; 379: lib_System.Security.Cryptography.OpenSsl.dll.so => 123
	i64 u0x56442b99bc64bb47, ; 380: System.Runtime.Serialization.Xml.dll => 114
	i64 u0x56a8b26e1aeae27b, ; 381: System.Threading.Tasks.Dataflow => 140
	i64 u0x56f932d61e93c07f, ; 382: System.Globalization.Extensions => 40
	i64 u0x571c5cfbec5ae8e2, ; 383: System.Private.Uri => 86
	i64 u0x576499c9f52fea31, ; 384: Xamarin.AndroidX.Annotation => 245
	i64 u0x579a06fed6eec900, ; 385: System.Private.CoreLib.dll => 171
	i64 u0x57c542c14049b66d, ; 386: System.Diagnostics.DiagnosticSource => 27
	i64 u0x581a8bd5cfda563e, ; 387: System.Threading.Timer => 146
	i64 u0x584ac38e21d2fde1, ; 388: Microsoft.Extensions.Configuration.Binder => 195
	i64 u0x58601b2dda4a27b9, ; 389: lib-ja-Microsoft.Maui.Controls.resources.dll.so => 345
	i64 u0x58688d9af496b168, ; 390: Microsoft.Extensions.DependencyInjection.dll => 198
	i64 u0x588c167a79db6bfb, ; 391: lib_Xamarin.Google.ErrorProne.Annotations.dll.so => 318
	i64 u0x5906028ae5151104, ; 392: Xamarin.AndroidX.Activity.Ktx => 244
	i64 u0x595a356d23e8da9a, ; 393: lib_Microsoft.CSharp.dll.so => 1
	i64 u0x59f9e60b9475085f, ; 394: lib_Xamarin.AndroidX.Annotation.Experimental.dll.so => 246
	i64 u0x5a70033ca9d003cb, ; 395: lib_System.Memory.Data.dll.so => 233
	i64 u0x5a745f5101a75527, ; 396: lib_System.IO.Compression.FileSystem.dll.so => 43
	i64 u0x5a89a886ae30258d, ; 397: lib_Xamarin.AndroidX.CoordinatorLayout.dll.so => 260
	i64 u0x5a8f6699f4a1caa9, ; 398: lib_System.Threading.dll.so => 147
	i64 u0x5ae8e4f3eae4d547, ; 399: Xamarin.AndroidX.Legacy.Support.Core.Utils => 275
	i64 u0x5ae9cd33b15841bf, ; 400: System.ComponentModel => 18
	i64 u0x5b54391bdc6fcfe6, ; 401: System.Private.DataContractSerialization => 85
	i64 u0x5b5f0e240a06a2a2, ; 402: da/Microsoft.Maui.Controls.resources.dll => 333
	i64 u0x5b8109e8e14c5e3e, ; 403: System.Globalization.Extensions.dll => 40
	i64 u0x5bddd04d72a9e350, ; 404: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 280
	i64 u0x5bdf16b09da116ab, ; 405: Xamarin.AndroidX.Collection => 254
	i64 u0x5c019d5266093159, ; 406: lib_Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android.dll.so => 285
	i64 u0x5c30a4a35f9cc8c4, ; 407: lib_System.Reflection.Extensions.dll.so => 93
	i64 u0x5c393624b8176517, ; 408: lib_Microsoft.Extensions.Logging.dll.so => 207
	i64 u0x5c53c29f5073b0c9, ; 409: System.Diagnostics.FileVersionInfo => 28
	i64 u0x5c87463c575c7616, ; 410: lib_System.Globalization.Extensions.dll.so => 40
	i64 u0x5d0a4a29b02d9d3c, ; 411: System.Net.WebHeaderCollection.dll => 77
	i64 u0x5d25ef991dd9a85c, ; 412: Microsoft.AspNetCore.Components.WebView.Maui.dll => 180
	i64 u0x5d40c9b15181641f, ; 413: lib_Xamarin.AndroidX.Emoji2.dll.so => 269
	i64 u0x5d6ca10d35e9485b, ; 414: lib_Xamarin.AndroidX.Concurrent.Futures.dll.so => 257
	i64 u0x5d7ec76c1c703055, ; 415: System.Threading.Tasks.Parallel => 142
	i64 u0x5db0cbbd1028510e, ; 416: lib_System.Runtime.InteropServices.dll.so => 107
	i64 u0x5db30905d3e5013b, ; 417: Xamarin.AndroidX.Collection.Jvm.dll => 255
	i64 u0x5e467bc8f09ad026, ; 418: System.Collections.Specialized.dll => 11
	i64 u0x5e5173b3208d97e7, ; 419: System.Runtime.Handles.dll => 104
	i64 u0x5ea92fdb19ec8c4c, ; 420: System.Text.Encodings.Web.dll => 136
	i64 u0x5eb8046dd40e9ac3, ; 421: System.ComponentModel.Primitives => 16
	i64 u0x5ec272d219c9aba4, ; 422: System.Security.Cryptography.Csp.dll => 121
	i64 u0x5eee1376d94c7f5e, ; 423: System.Net.HttpListener.dll => 65
	i64 u0x5f36ccf5c6a57e24, ; 424: System.Xml.ReaderWriter.dll => 155
	i64 u0x5f4294b9b63cb842, ; 425: System.Data.Common => 22
	i64 u0x5f9a2d823f664957, ; 426: lib-el-Microsoft.Maui.Controls.resources.dll.so => 335
	i64 u0x5fa6da9c3cd8142a, ; 427: lib_Xamarin.KotlinX.Serialization.Core.dll.so => 328
	i64 u0x5fac98e0b37a5b9d, ; 428: System.Runtime.CompilerServices.Unsafe.dll => 101
	i64 u0x609f4b7b63d802d4, ; 429: lib_Microsoft.Extensions.DependencyInjection.dll.so => 198
	i64 u0x60cd4e33d7e60134, ; 430: Xamarin.KotlinX.Coroutines.Core.Jvm => 327
	i64 u0x60f62d786afcf130, ; 431: System.Memory => 62
	i64 u0x61bb78c89f867353, ; 432: System.IO => 57
	i64 u0x61be8d1299194243, ; 433: Microsoft.Maui.Controls.Xaml => 222
	i64 u0x61d2cba29557038f, ; 434: de/Microsoft.Maui.Controls.resources => 334
	i64 u0x61d88f399afb2f45, ; 435: lib_System.Runtime.Loader.dll.so => 109
	i64 u0x622eef6f9e59068d, ; 436: System.Private.CoreLib => 171
	i64 u0x63d5e3aa4ef9b931, ; 437: Xamarin.KotlinX.Coroutines.Android.dll => 325
	i64 u0x63f1f6883c1e23c2, ; 438: lib_System.Collections.Immutable.dll.so => 9
	i64 u0x6400f68068c1e9f1, ; 439: Xamarin.Google.Android.Material.dll => 315
	i64 u0x640e3b14dbd325c2, ; 440: System.Security.Cryptography.Algorithms.dll => 119
	i64 u0x64587004560099b9, ; 441: System.Reflection => 97
	i64 u0x64b1529a438a3c45, ; 442: lib_System.Runtime.Handles.dll.so => 104
	i64 u0x6565fba2cd8f235b, ; 443: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 288
	i64 u0x65ecac39144dd3cc, ; 444: Microsoft.Maui.Controls.dll => 221
	i64 u0x65ece51227bfa724, ; 445: lib_System.Runtime.Numerics.dll.so => 110
	i64 u0x661722438787b57f, ; 446: Xamarin.AndroidX.Annotation.Jvm.dll => 247
	i64 u0x6679b2337ee6b22a, ; 447: lib_System.IO.FileSystem.Primitives.dll.so => 48
	i64 u0x6692e924eade1b29, ; 448: lib_System.Console.dll.so => 20
	i64 u0x66a4e5c6a3fb0bae, ; 449: lib_Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll.so => 287
	i64 u0x66ad21286ac74b9d, ; 450: lib_System.Drawing.Common.dll.so => 230
	i64 u0x66d13304ce1a3efa, ; 451: Xamarin.AndroidX.CursorAdapter => 263
	i64 u0x674303f65d8fad6f, ; 452: lib_System.Net.Quic.dll.so => 71
	i64 u0x6756ca4cad62e9d6, ; 453: lib_Xamarin.AndroidX.ConstraintLayout.Core.dll.so => 259
	i64 u0x67c0802770244408, ; 454: System.Windows.dll => 153
	i64 u0x68100b69286e27cd, ; 455: lib_System.Formats.Tar.dll.so => 38
	i64 u0x68558ec653afa616, ; 456: lib-da-Microsoft.Maui.Controls.resources.dll.so => 333
	i64 u0x6857d56b8e8b4bb6, ; 457: lib_Microsoft.AspNetCore.Metadata.dll.so => 184
	i64 u0x6872ec7a2e36b1ac, ; 458: System.Drawing.Primitives.dll => 35
	i64 u0x68bb2c417aa9b61c, ; 459: Xamarin.KotlinX.AtomicFU.dll => 323
	i64 u0x68fbbbe2eb455198, ; 460: System.Formats.Asn1 => 231
	i64 u0x69063fc0ba8e6bdd, ; 461: he/Microsoft.Maui.Controls.resources.dll => 339
	i64 u0x69a3e26c76f6eec4, ; 462: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 314
	i64 u0x6a4d7577b2317255, ; 463: System.Runtime.InteropServices.dll => 107
	i64 u0x6ace3b74b15ee4a4, ; 464: nb/Microsoft.Maui.Controls.resources => 348
	i64 u0x6afcedb171067e2b, ; 465: System.Core.dll => 21
	i64 u0x6bef98e124147c24, ; 466: Xamarin.Jetbrains.Annotations => 320
	i64 u0x6cd97f370311a542, ; 467: Microsoft.EntityFrameworkCore.SqlServer => 190
	i64 u0x6ce874bff138ce2b, ; 468: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 286
	i64 u0x6d0a12b2adba20d8, ; 469: System.Security.Cryptography.ProtectedData.dll => 235
	i64 u0x6d12bfaa99c72b1f, ; 470: lib_Microsoft.Maui.Graphics.dll.so => 225
	i64 u0x6d70755158ca866e, ; 471: lib_System.ComponentModel.EventBasedAsync.dll.so => 15
	i64 u0x6d79993361e10ef2, ; 472: Microsoft.Extensions.Primitives => 211
	i64 u0x6d7eeca99577fc8b, ; 473: lib_System.Net.WebProxy.dll.so => 78
	i64 u0x6d8515b19946b6a2, ; 474: System.Net.WebProxy.dll => 78
	i64 u0x6d86d56b84c8eb71, ; 475: lib_Xamarin.AndroidX.CursorAdapter.dll.so => 263
	i64 u0x6d9bea6b3e895cf7, ; 476: Microsoft.Extensions.Primitives.dll => 211
	i64 u0x6dd9bf4083de3f6a, ; 477: Xamarin.AndroidX.DocumentFile.dll => 266
	i64 u0x6e25a02c3833319a, ; 478: lib_Xamarin.AndroidX.Navigation.Fragment.dll.so => 293
	i64 u0x6e79c6bd8627412a, ; 479: Xamarin.AndroidX.SavedState.SavedState.Ktx => 301
	i64 u0x6e838d9a2a6f6c9e, ; 480: lib_System.ValueTuple.dll.so => 150
	i64 u0x6e9965ce1095e60a, ; 481: lib_System.Core.dll.so => 21
	i64 u0x6fd2265da78b93a4, ; 482: lib_Microsoft.Maui.dll.so => 223
	i64 u0x6fdfc7de82c33008, ; 483: cs/Microsoft.Maui.Controls.resources => 332
	i64 u0x6ffc4967cc47ba57, ; 484: System.IO.FileSystem.Watcher.dll => 49
	i64 u0x701cd46a1c25a5fe, ; 485: System.IO.FileSystem.dll => 50
	i64 u0x70c1154d9ce7bd51, ; 486: Xamarin.Kotlin.StdLib.Common.dll => 322
	i64 u0x70e99f48c05cb921, ; 487: tr/Microsoft.Maui.Controls.resources.dll => 358
	i64 u0x70fd3deda22442d2, ; 488: lib-nb-Microsoft.Maui.Controls.resources.dll.so => 348
	i64 u0x71485e7ffdb4b958, ; 489: System.Reflection.Extensions => 93
	i64 u0x7162a2fce67a945f, ; 490: lib_Xamarin.Android.Glide.Annotations.dll.so => 240
	i64 u0x71a495ea3761dde8, ; 491: lib-it-Microsoft.Maui.Controls.resources.dll.so => 344
	i64 u0x71ad672adbe48f35, ; 492: System.ComponentModel.Primitives.dll => 16
	i64 u0x725f5a9e82a45c81, ; 493: System.Security.Cryptography.Encoding => 122
	i64 u0x72b1fb4109e08d7b, ; 494: lib-hr-Microsoft.Maui.Controls.resources.dll.so => 341
	i64 u0x72e0300099accce1, ; 495: System.Xml.XPath.XDocument => 158
	i64 u0x730bfb248998f67a, ; 496: System.IO.Compression.ZipFile => 44
	i64 u0x732b2d67b9e5c47b, ; 497: Xamarin.Google.ErrorProne.Annotations.dll => 318
	i64 u0x734b76fdc0dc05bb, ; 498: lib_GoogleGson.dll.so => 174
	i64 u0x73a6be34e822f9d1, ; 499: lib_System.Runtime.Serialization.dll.so => 115
	i64 u0x73e4ce94e2eb6ffc, ; 500: lib_System.Memory.dll.so => 62
	i64 u0x743a1eccf080489a, ; 501: WindowsBase.dll => 164
	i64 u0x7465c42afc9ef57e, ; 502: Microsoft.AspNetCore.Identity.EntityFrameworkCore => 183
	i64 u0x755a91767330b3d4, ; 503: lib_Microsoft.Extensions.Configuration.dll.so => 193
	i64 u0x75c326eb821b85c4, ; 504: lib_System.ComponentModel.DataAnnotations.dll.so => 14
	i64 u0x76012e7334db86e5, ; 505: lib_Xamarin.AndroidX.SavedState.dll.so => 300
	i64 u0x76ca07b878f44da0, ; 506: System.Runtime.Numerics.dll => 110
	i64 u0x7736c8a96e51a061, ; 507: lib_Xamarin.AndroidX.Annotation.Jvm.dll.so => 247
	i64 u0x778a805e625329ef, ; 508: System.Linq.Parallel => 59
	i64 u0x779290cc2b801eb7, ; 509: Xamarin.KotlinX.AtomicFU.Jvm => 324
	i64 u0x779f67ad3b8efbd5, ; 510: Microsoft.Extensions.Configuration.Json.dll => 197
	i64 u0x77f8a4acc2fdc449, ; 511: System.Security.Cryptography.Cng.dll => 120
	i64 u0x780bc73597a503a9, ; 512: lib-ms-Microsoft.Maui.Controls.resources.dll.so => 347
	i64 u0x782c5d8eb99ff201, ; 513: lib_Microsoft.VisualBasic.Core.dll.so => 2
	i64 u0x783606d1e53e7a1a, ; 514: th/Microsoft.Maui.Controls.resources.dll => 357
	i64 u0x7841c47b741b9f64, ; 515: System.Security.Permissions => 236
	i64 u0x78a45e51311409b6, ; 516: Xamarin.AndroidX.Fragment.dll => 272
	i64 u0x78ed4ab8f9d800a1, ; 517: Xamarin.AndroidX.Lifecycle.ViewModel => 286
	i64 u0x79f2a1023f4320f2, ; 518: Microsoft.Win32.SystemEvents => 227
	i64 u0x7a39601d6f0bb831, ; 519: lib_Xamarin.KotlinX.AtomicFU.dll.so => 323
	i64 u0x7a71889545dcdb00, ; 520: lib_Microsoft.AspNetCore.Components.WebView.dll.so => 179
	i64 u0x7a7e7eddf79c5d26, ; 521: lib_Xamarin.AndroidX.Lifecycle.ViewModel.dll.so => 286
	i64 u0x7a9a57d43b0845fa, ; 522: System.AppContext => 6
	i64 u0x7ad0f4f1e5d08183, ; 523: Xamarin.AndroidX.Collection.dll => 254
	i64 u0x7adb8da2ac89b647, ; 524: fi/Microsoft.Maui.Controls.resources.dll => 337
	i64 u0x7b13d9eaa944ade8, ; 525: Xamarin.AndroidX.DynamicAnimation.dll => 268
	i64 u0x7b4927e421291c41, ; 526: Microsoft.IdentityModel.JsonWebTokens.dll => 215
	i64 u0x7bef86a4335c4870, ; 527: System.ComponentModel.TypeConverter => 17
	i64 u0x7c0820144cd34d6a, ; 528: sk/Microsoft.Maui.Controls.resources.dll => 355
	i64 u0x7c2a0bd1e0f988fc, ; 529: lib-de-Microsoft.Maui.Controls.resources.dll.so => 334
	i64 u0x7c41d387501568ba, ; 530: System.Net.WebClient.dll => 76
	i64 u0x7c482cd79bd24b13, ; 531: lib_Xamarin.AndroidX.ConstraintLayout.dll.so => 258
	i64 u0x7c4867f3cb880d2f, ; 532: Microsoft.AspNetCore.Metadata => 184
	i64 u0x7cd2ec8eaf5241cd, ; 533: System.Security.dll => 130
	i64 u0x7cf9ae50dd350622, ; 534: Xamarin.Jetbrains.Annotations.dll => 320
	i64 u0x7d649b75d580bb42, ; 535: ms/Microsoft.Maui.Controls.resources.dll => 347
	i64 u0x7d8b5821548f89e7, ; 536: Microsoft.AspNetCore.Components.Forms => 177
	i64 u0x7d8ee2bdc8e3aad1, ; 537: System.Numerics.Vectors => 82
	i64 u0x7df5df8db8eaa6ac, ; 538: Microsoft.Extensions.Logging.Debug => 209
	i64 u0x7dfc3d6d9d8d7b70, ; 539: System.Collections => 12
	i64 u0x7e2e564fa2f76c65, ; 540: lib_System.Diagnostics.Tracing.dll.so => 34
	i64 u0x7e302e110e1e1346, ; 541: lib_System.Security.Claims.dll.so => 118
	i64 u0x7e4465b3f78ad8d0, ; 542: Xamarin.KotlinX.Serialization.Core.dll => 328
	i64 u0x7e571cad5915e6c3, ; 543: lib_Xamarin.AndroidX.Lifecycle.Process.dll.so => 281
	i64 u0x7e6b1ca712437d7d, ; 544: Xamarin.AndroidX.Emoji2.ViewsHelper => 270
	i64 u0x7e946809d6008ef2, ; 545: lib_System.ObjectModel.dll.so => 84
	i64 u0x7ea0272c1b4a9635, ; 546: lib_Xamarin.Android.Glide.dll.so => 239
	i64 u0x7ebe6126501e1198, ; 547: Microsoft.AspNetCore.Cryptography.KeyDerivation.dll => 182
	i64 u0x7ecc13347c8fd849, ; 548: lib_System.ComponentModel.dll.so => 18
	i64 u0x7f00ddd9b9ca5a13, ; 549: Xamarin.AndroidX.ViewPager.dll => 311
	i64 u0x7f9351cd44b1273f, ; 550: Microsoft.Extensions.Configuration.Abstractions => 194
	i64 u0x7fae0ef4dc4770fe, ; 551: Microsoft.Identity.Client => 212
	i64 u0x7fbd557c99b3ce6f, ; 552: lib_Xamarin.AndroidX.Lifecycle.LiveData.Core.dll.so => 279
	i64 u0x8076a9a44a2ca331, ; 553: System.Net.Quic => 71
	i64 u0x80b7e726b0280681, ; 554: Microsoft.VisualStudio.DesignTools.MobileTapContracts => 365
	i64 u0x80da183a87731838, ; 555: System.Reflection.Metadata => 94
	i64 u0x8101a73bd4533440, ; 556: Microsoft.AspNetCore.Components.Web => 178
	i64 u0x812c069d5cdecc17, ; 557: System.dll => 163
	i64 u0x81381be520a60adb, ; 558: Xamarin.AndroidX.Interpolator.dll => 274
	i64 u0x81657cec2b31e8aa, ; 559: System.Net => 81
	i64 u0x81ab745f6c0f5ce6, ; 560: zh-Hant/Microsoft.Maui.Controls.resources => 363
	i64 u0x8277f2be6b5ce05f, ; 561: Xamarin.AndroidX.AppCompat => 248
	i64 u0x828f06563b30bc50, ; 562: lib_Xamarin.AndroidX.CardView.dll.so => 253
	i64 u0x82920a8d9194a019, ; 563: Xamarin.KotlinX.AtomicFU.Jvm.dll => 324
	i64 u0x82b399cb01b531c4, ; 564: lib_System.Web.dll.so => 152
	i64 u0x82df8f5532a10c59, ; 565: lib_System.Drawing.dll.so => 36
	i64 u0x82f0b6e911d13535, ; 566: lib_System.Transactions.dll.so => 149
	i64 u0x82f6403342e12049, ; 567: uk/Microsoft.Maui.Controls.resources => 359
	i64 u0x83a7afd2c49adc86, ; 568: lib_Microsoft.IdentityModel.Abstractions.dll.so => 214
	i64 u0x83c14ba66c8e2b8c, ; 569: zh-Hans/Microsoft.Maui.Controls.resources => 362
	i64 u0x83de69860da6cbdd, ; 570: Microsoft.Extensions.FileProviders.Composite => 201
	i64 u0x846ce984efea52c7, ; 571: System.Threading.Tasks.Parallel.dll => 142
	i64 u0x84ae73148a4557d2, ; 572: lib_System.IO.Pipes.dll.so => 55
	i64 u0x84b01102c12a9232, ; 573: System.Runtime.Serialization.Json.dll => 112
	i64 u0x84cd5cdec0f54bcc, ; 574: lib_Microsoft.EntityFrameworkCore.Relational.dll.so => 189
	i64 u0x850c5ba0b57ce8e7, ; 575: lib_Xamarin.AndroidX.Collection.dll.so => 254
	i64 u0x851d02edd334b044, ; 576: Xamarin.AndroidX.VectorDrawable => 308
	i64 u0x85c919db62150978, ; 577: Xamarin.AndroidX.Transition.dll => 307
	i64 u0x8662aaeb94fef37f, ; 578: lib_System.Dynamic.Runtime.dll.so => 37
	i64 u0x86a909228dc7657b, ; 579: lib-zh-Hant-Microsoft.Maui.Controls.resources.dll.so => 363
	i64 u0x86b3e00c36b84509, ; 580: Microsoft.Extensions.Configuration.dll => 193
	i64 u0x86b62cb077ec4fd7, ; 581: System.Runtime.Serialization.Xml => 114
	i64 u0x8704193f462e892e, ; 582: lib_Microsoft.Extensions.FileSystemGlobbing.dll.so => 204
	i64 u0x8706ffb12bf3f53d, ; 583: Xamarin.AndroidX.Annotation.Experimental => 246
	i64 u0x872a5b14c18d328c, ; 584: System.ComponentModel.DataAnnotations => 14
	i64 u0x872fb9615bc2dff0, ; 585: Xamarin.Android.Glide.Annotations.dll => 240
	i64 u0x87c4b8a492b176ad, ; 586: Microsoft.EntityFrameworkCore.Abstractions => 188
	i64 u0x87c69b87d9283884, ; 587: lib_System.Threading.Thread.dll.so => 144
	i64 u0x87f6569b25707834, ; 588: System.IO.Compression.Brotli.dll => 42
	i64 u0x8842b3a5d2d3fb36, ; 589: Microsoft.Maui.Essentials => 224
	i64 u0x88926583efe7ee86, ; 590: Xamarin.AndroidX.Activity.Ktx.dll => 244
	i64 u0x88ba6bc4f7762b03, ; 591: lib_System.Reflection.dll.so => 97
	i64 u0x88bda98e0cffb7a9, ; 592: lib_Xamarin.KotlinX.Coroutines.Core.Jvm.dll.so => 327
	i64 u0x8930322c7bd8f768, ; 593: netstandard => 166
	i64 u0x897a606c9e39c75f, ; 594: lib_System.ComponentModel.Primitives.dll.so => 16
	i64 u0x89911a22005b92b7, ; 595: System.IO.FileSystem.DriveInfo.dll => 47
	i64 u0x89c5188089ec2cd5, ; 596: lib_System.Runtime.InteropServices.RuntimeInformation.dll.so => 106
	i64 u0x8a19e3dc71b34b2c, ; 597: System.Reflection.TypeExtensions.dll => 96
	i64 u0x8a399a706fcbce4b, ; 598: Microsoft.Extensions.Caching.Abstractions => 191
	i64 u0x8ad229ea26432ee2, ; 599: Xamarin.AndroidX.Loader => 290
	i64 u0x8b4ff5d0fdd5faa1, ; 600: lib_System.Diagnostics.DiagnosticSource.dll.so => 27
	i64 u0x8b541d476eb3774c, ; 601: System.Security.Principal.Windows => 127
	i64 u0x8b8d01333a96d0b5, ; 602: System.Diagnostics.Process.dll => 29
	i64 u0x8b9ceca7acae3451, ; 603: lib-he-Microsoft.Maui.Controls.resources.dll.so => 339
	i64 u0x8ba96f31f69ece34, ; 604: Microsoft.Win32.SystemEvents.dll => 227
	i64 u0x8c53ae18581b14f0, ; 605: Azure.Core => 172
	i64 u0x8c575135aa1ccef4, ; 606: Microsoft.Extensions.FileProviders.Abstractions => 200
	i64 u0x8cb8f612b633affb, ; 607: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 301
	i64 u0x8cdfdb4ce85fb925, ; 608: lib_System.Security.Principal.Windows.dll.so => 127
	i64 u0x8cdfe7b8f4caa426, ; 609: System.IO.Compression.FileSystem => 43
	i64 u0x8cf51f1eb9e90658, ; 610: lib_Microsoft.EntityFrameworkCore.SqlServer.dll.so => 190
	i64 u0x8d0f420977c2c1c7, ; 611: Xamarin.AndroidX.CursorAdapter.dll => 263
	i64 u0x8d52f7ea2796c531, ; 612: Xamarin.AndroidX.Emoji2.dll => 269
	i64 u0x8d7b8ab4b3310ead, ; 613: System.Threading => 147
	i64 u0x8da188285aadfe8e, ; 614: System.Collections.Concurrent => 8
	i64 u0x8e937db395a74375, ; 615: lib_Microsoft.Identity.Client.dll.so => 212
	i64 u0x8ed807bfe9858dfc, ; 616: Xamarin.AndroidX.Navigation.Common => 292
	i64 u0x8ee08b8194a30f48, ; 617: lib-hi-Microsoft.Maui.Controls.resources.dll.so => 340
	i64 u0x8ef7601039857a44, ; 618: lib-ro-Microsoft.Maui.Controls.resources.dll.so => 353
	i64 u0x8f32c6f611f6ffab, ; 619: pt/Microsoft.Maui.Controls.resources.dll => 352
	i64 u0x8f44b45eb046bbd1, ; 620: System.ServiceModel.Web.dll => 131
	i64 u0x8f8829d21c8985a4, ; 621: lib-pt-BR-Microsoft.Maui.Controls.resources.dll.so => 351
	i64 u0x8fbf5b0114c6dcef, ; 622: System.Globalization.dll => 41
	i64 u0x8fcc8c2a81f3d9e7, ; 623: Xamarin.KotlinX.Serialization.Core => 328
	i64 u0x90263f8448b8f572, ; 624: lib_System.Diagnostics.TraceSource.dll.so => 33
	i64 u0x903101b46fb73a04, ; 625: _Microsoft.Android.Resource.Designer => 368
	i64 u0x90393bd4865292f3, ; 626: lib_System.IO.Compression.dll.so => 45
	i64 u0x905e2b8e7ae91ae6, ; 627: System.Threading.Tasks.Extensions.dll => 141
	i64 u0x90634f86c5ebe2b5, ; 628: Xamarin.AndroidX.Lifecycle.ViewModel.Android => 287
	i64 u0x907b636704ad79ef, ; 629: lib_Microsoft.Maui.Controls.Xaml.dll.so => 222
	i64 u0x90e9efbfd68593e0, ; 630: lib_Xamarin.AndroidX.Lifecycle.LiveData.dll.so => 278
	i64 u0x91418dc638b29e68, ; 631: lib_Xamarin.AndroidX.CustomView.dll.so => 264
	i64 u0x914647982e998267, ; 632: Microsoft.Extensions.Configuration.Json => 197
	i64 u0x9157bd523cd7ed36, ; 633: lib_System.Text.Json.dll.so => 237
	i64 u0x91a74f07b30d37e2, ; 634: System.Linq.dll => 61
	i64 u0x91cb86ea3b17111d, ; 635: System.ServiceModel.Web => 131
	i64 u0x91fa41a87223399f, ; 636: ca/Microsoft.Maui.Controls.resources.dll => 331
	i64 u0x92054e486c0c7ea7, ; 637: System.IO.FileSystem.DriveInfo => 47
	i64 u0x928614058c40c4cd, ; 638: lib_System.Xml.XPath.XDocument.dll.so => 158
	i64 u0x92b138fffca2b01e, ; 639: lib_Xamarin.AndroidX.Arch.Core.Runtime.dll.so => 251
	i64 u0x92dfc2bfc6c6a888, ; 640: Xamarin.AndroidX.Lifecycle.LiveData => 278
	i64 u0x933da2c779423d68, ; 641: Xamarin.Android.Glide.Annotations => 240
	i64 u0x9388aad9b7ae40ce, ; 642: lib_Xamarin.AndroidX.Lifecycle.Common.dll.so => 276
	i64 u0x93cfa73ab28d6e35, ; 643: ms/Microsoft.Maui.Controls.resources => 347
	i64 u0x941c00d21e5c0679, ; 644: lib_Xamarin.AndroidX.Transition.dll.so => 307
	i64 u0x944077d8ca3c6580, ; 645: System.IO.Compression.dll => 45
	i64 u0x948cffedc8ed7960, ; 646: System.Xml => 162
	i64 u0x948d746a7702861f, ; 647: Microsoft.IdentityModel.Logging.dll => 216
	i64 u0x94c8990839c4bdb1, ; 648: lib_Xamarin.AndroidX.Interpolator.dll.so => 274
	i64 u0x9502fd818eed2359, ; 649: lib_Microsoft.IdentityModel.Protocols.OpenIdConnect.dll.so => 218
	i64 u0x9564283c37ed59a9, ; 650: lib_Microsoft.IdentityModel.Logging.dll.so => 216
	i64 u0x967fc325e09bfa8c, ; 651: es/Microsoft.Maui.Controls.resources => 336
	i64 u0x9686161486d34b81, ; 652: lib_Xamarin.AndroidX.ExifInterface.dll.so => 271
	i64 u0x96e49b31fe33d427, ; 653: Microsoft.Identity.Client.Extensions.Msal => 213
	i64 u0x9732d8dbddea3d9a, ; 654: id/Microsoft.Maui.Controls.resources => 343
	i64 u0x978be80e5210d31b, ; 655: Microsoft.Maui.Graphics.dll => 225
	i64 u0x97b8c771ea3e4220, ; 656: System.ComponentModel.dll => 18
	i64 u0x97e144c9d3c6976e, ; 657: System.Collections.Concurrent.dll => 8
	i64 u0x984184e3c70d4419, ; 658: GoogleGson => 174
	i64 u0x9843944103683dd3, ; 659: Xamarin.AndroidX.Core.Core.Ktx => 262
	i64 u0x98d720cc4597562c, ; 660: System.Security.Cryptography.OpenSsl => 123
	i64 u0x991d510397f92d9d, ; 661: System.Linq.Expressions => 58
	i64 u0x996ceeb8a3da3d67, ; 662: System.Threading.Overlapped.dll => 139
	i64 u0x99a00ca5270c6878, ; 663: Xamarin.AndroidX.Navigation.Runtime => 294
	i64 u0x99cdc6d1f2d3a72f, ; 664: ko/Microsoft.Maui.Controls.resources.dll => 346
	i64 u0x9a01b1da98b6ee10, ; 665: Xamarin.AndroidX.Lifecycle.Runtime.dll => 282
	i64 u0x9a0cc42c6f36dfc9, ; 666: lib_Microsoft.IdentityModel.Protocols.dll.so => 217
	i64 u0x9a5ccc274fd6e6ee, ; 667: Jsr305Binding.dll => 316
	i64 u0x9ae6940b11c02876, ; 668: lib_Xamarin.AndroidX.Window.dll.so => 313
	i64 u0x9b211a749105beac, ; 669: System.Transactions.Local => 148
	i64 u0x9b8734714671022d, ; 670: System.Threading.Tasks.Dataflow.dll => 140
	i64 u0x9bc6aea27fbf034f, ; 671: lib_Xamarin.KotlinX.Coroutines.Core.dll.so => 326
	i64 u0x9bd8cc74558ad4c7, ; 672: Xamarin.KotlinX.AtomicFU => 323
	i64 u0x9c244ac7cda32d26, ; 673: System.Security.Cryptography.X509Certificates.dll => 125
	i64 u0x9c465f280cf43733, ; 674: lib_Xamarin.KotlinX.Coroutines.Android.dll.so => 325
	i64 u0x9c8f6872beab6408, ; 675: System.Xml.XPath.XDocument.dll => 158
	i64 u0x9ce01cf91101ae23, ; 676: System.Xml.XmlDocument => 160
	i64 u0x9d128180c81d7ce6, ; 677: Xamarin.AndroidX.CustomView.PoolingContainer => 265
	i64 u0x9d5dbcf5a48583fe, ; 678: lib_Xamarin.AndroidX.Activity.dll.so => 243
	i64 u0x9d74dee1a7725f34, ; 679: Microsoft.Extensions.Configuration.Abstractions.dll => 194
	i64 u0x9e4534b6adaf6e84, ; 680: nl/Microsoft.Maui.Controls.resources => 349
	i64 u0x9e4b95dec42769f7, ; 681: System.Diagnostics.Debug.dll => 26
	i64 u0x9eaf1efdf6f7267e, ; 682: Xamarin.AndroidX.Navigation.Common.dll => 292
	i64 u0x9ef542cf1f78c506, ; 683: Xamarin.AndroidX.Lifecycle.LiveData.Core => 279
	i64 u0x9fbb2961ca18e5c2, ; 684: Microsoft.Extensions.FileProviders.Physical.dll => 203
	i64 u0x9ffbb6b1434ad2df, ; 685: Microsoft.Identity.Client.dll => 212
	i64 u0xa00832eb975f56a8, ; 686: lib_System.Net.dll.so => 81
	i64 u0xa0ad78236b7b267f, ; 687: Xamarin.AndroidX.Window => 313
	i64 u0xa0d8259f4cc284ec, ; 688: lib_System.Security.Cryptography.dll.so => 126
	i64 u0xa0e17ca50c77a225, ; 689: lib_Xamarin.Google.Crypto.Tink.Android.dll.so => 317
	i64 u0xa0ff9b3e34d92f11, ; 690: lib_System.Resources.Writer.dll.so => 100
	i64 u0xa12fbfb4da97d9f3, ; 691: System.Threading.Timer.dll => 146
	i64 u0xa1440773ee9d341e, ; 692: Xamarin.Google.Android.Material => 315
	i64 u0xa1b9d7c27f47219f, ; 693: Xamarin.AndroidX.Navigation.UI.dll => 295
	i64 u0xa2572680829d2c7c, ; 694: System.IO.Pipelines.dll => 53
	i64 u0xa26597e57ee9c7f6, ; 695: System.Xml.XmlDocument.dll => 160
	i64 u0xa308401900e5bed3, ; 696: lib_mscorlib.dll.so => 165
	i64 u0xa395572e7da6c99d, ; 697: lib_System.Security.dll.so => 130
	i64 u0xa3b8104115a36bf6, ; 698: lib_Microsoft.Extensions.FileProviders.Embedded.dll.so => 202
	i64 u0xa3e683f24b43af6f, ; 699: System.Dynamic.Runtime.dll => 37
	i64 u0xa4145becdee3dc4f, ; 700: Xamarin.AndroidX.VectorDrawable.Animated => 309
	i64 u0xa46aa1eaa214539b, ; 701: ko/Microsoft.Maui.Controls.resources => 346
	i64 u0xa4e62983cf1e3674, ; 702: Microsoft.AspNetCore.Components.Forms.dll => 177
	i64 u0xa4edc8f2ceae241a, ; 703: System.Data.Common.dll => 22
	i64 u0xa526fadd66308051, ; 704: Microsoft.EntityFrameworkCore.SqlServer.dll => 190
	i64 u0xa5494f40f128ce6a, ; 705: System.Runtime.Serialization.Formatters.dll => 111
	i64 u0xa54b74df83dce92b, ; 706: System.Reflection.DispatchProxy => 89
	i64 u0xa579ed010d7e5215, ; 707: Xamarin.AndroidX.DocumentFile => 266
	i64 u0xa5b7152421ed6d98, ; 708: lib_System.IO.FileSystem.Watcher.dll.so => 49
	i64 u0xa5c3844f17b822db, ; 709: lib_System.Linq.Parallel.dll.so => 59
	i64 u0xa5ce5c755bde8cb8, ; 710: lib_System.Security.Cryptography.Csp.dll.so => 121
	i64 u0xa5e599d1e0524750, ; 711: System.Numerics.Vectors.dll => 82
	i64 u0xa5f1ba49b85dd355, ; 712: System.Security.Cryptography.dll => 126
	i64 u0xa61975a5a37873ea, ; 713: lib_System.Xml.XmlSerializer.dll.so => 161
	i64 u0xa6593e21584384d2, ; 714: lib_Jsr305Binding.dll.so => 316
	i64 u0xa66cbee0130865f7, ; 715: lib_WindowsBase.dll.so => 164
	i64 u0xa67dbee13e1df9ca, ; 716: Xamarin.AndroidX.SavedState.dll => 300
	i64 u0xa684b098dd27b296, ; 717: lib_Xamarin.AndroidX.Security.SecurityCrypto.dll.so => 302
	i64 u0xa68a420042bb9b1f, ; 718: Xamarin.AndroidX.DrawerLayout.dll => 267
	i64 u0xa6d26156d1cacc7c, ; 719: Xamarin.Android.Glide.dll => 239
	i64 u0xa71fe7d6f6f93efd, ; 720: Microsoft.Data.SqlClient => 186
	i64 u0xa75386b5cb9595aa, ; 721: Xamarin.AndroidX.Lifecycle.Runtime.Android => 283
	i64 u0xa763fbb98df8d9fb, ; 722: lib_Microsoft.Win32.Primitives.dll.so => 4
	i64 u0xa78ce3745383236a, ; 723: Xamarin.AndroidX.Lifecycle.Common.Jvm => 277
	i64 u0xa7c31b56b4dc7b33, ; 724: hu/Microsoft.Maui.Controls.resources => 342
	i64 u0xa7eab29ed44b4e7a, ; 725: Mono.Android.Export => 168
	i64 u0xa8195217cbf017b7, ; 726: Microsoft.VisualBasic.Core => 2
	i64 u0xa82fd211eef00a5b, ; 727: Microsoft.Extensions.FileProviders.Physical => 203
	i64 u0xa859a95830f367ff, ; 728: lib_Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll.so => 288
	i64 u0xa8b52f21e0dbe690, ; 729: System.Runtime.Serialization.dll => 115
	i64 u0xa8e6320dd07580ef, ; 730: lib_Microsoft.IdentityModel.JsonWebTokens.dll.so => 215
	i64 u0xa8ee4ed7de2efaee, ; 731: Xamarin.AndroidX.Annotation.dll => 245
	i64 u0xa923a621d2e9302a, ; 732: lib_SMITCOMIDAS.dll.so => 0
	i64 u0xa95590e7c57438a4, ; 733: System.Configuration => 19
	i64 u0xaa2219c8e3449ff5, ; 734: Microsoft.Extensions.Logging.Abstractions => 208
	i64 u0xaa443ac34067eeef, ; 735: System.Private.Xml.dll => 88
	i64 u0xaa52de307ef5d1dd, ; 736: System.Net.Http => 64
	i64 u0xaa8448d5c2540403, ; 737: System.Windows.Extensions => 238
	i64 u0xaa9a7b0214a5cc5c, ; 738: System.Diagnostics.StackTrace.dll => 30
	i64 u0xaaaf86367285a918, ; 739: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 199
	i64 u0xaaf84bb3f052a265, ; 740: el/Microsoft.Maui.Controls.resources => 335
	i64 u0xab9af77b5b67a0b8, ; 741: Xamarin.AndroidX.ConstraintLayout.Core => 259
	i64 u0xab9c1b2687d86b0b, ; 742: lib_System.Linq.Expressions.dll.so => 58
	i64 u0xac2af3fa195a15ce, ; 743: System.Runtime.Numerics => 110
	i64 u0xac5376a2a538dc10, ; 744: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 279
	i64 u0xac5acae88f60357e, ; 745: System.Diagnostics.Tools.dll => 32
	i64 u0xac79c7e46047ad98, ; 746: System.Security.Principal.Windows.dll => 127
	i64 u0xac98d31068e24591, ; 747: System.Xml.XDocument => 157
	i64 u0xacd46e002c3ccb97, ; 748: ro/Microsoft.Maui.Controls.resources => 353
	i64 u0xacdd9e4180d56dda, ; 749: Xamarin.AndroidX.Concurrent.Futures => 257
	i64 u0xacf42eea7ef9cd12, ; 750: System.Threading.Channels => 138
	i64 u0xad7e82ed3b0f16d0, ; 751: lib_Xamarin.AndroidX.DocumentFile.dll.so => 266
	i64 u0xad89c07347f1bad6, ; 752: nl/Microsoft.Maui.Controls.resources.dll => 349
	i64 u0xadbb53caf78a79d2, ; 753: System.Web.HttpUtility => 151
	i64 u0xadc90ab061a9e6e4, ; 754: System.ComponentModel.TypeConverter.dll => 17
	i64 u0xadca1b9030b9317e, ; 755: Xamarin.AndroidX.Collection.Ktx => 256
	i64 u0xadd8eda2edf396ad, ; 756: Xamarin.Android.Glide.GifDecoder => 242
	i64 u0xadf4cf30debbeb9a, ; 757: System.Net.ServicePoint.dll => 74
	i64 u0xadf511667bef3595, ; 758: System.Net.Security => 73
	i64 u0xae0aaa94fdcfce0f, ; 759: System.ComponentModel.EventBasedAsync.dll => 15
	i64 u0xae282bcd03739de7, ; 760: Java.Interop => 167
	i64 u0xae53579c90db1107, ; 761: System.ObjectModel.dll => 84
	i64 u0xaec7c0c7e2ed4575, ; 762: lib_Xamarin.KotlinX.AtomicFU.Jvm.dll.so => 324
	i64 u0xaf732d0b2193b8f5, ; 763: System.Security.Cryptography.OpenSsl.dll => 123
	i64 u0xafdb94dbccd9d11c, ; 764: Xamarin.AndroidX.Lifecycle.LiveData.dll => 278
	i64 u0xafe29f45095518e7, ; 765: lib_Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll.so => 289
	i64 u0xb03ae931fb25607e, ; 766: Xamarin.AndroidX.ConstraintLayout => 258
	i64 u0xb05cc42cd94c6d9d, ; 767: lib-sv-Microsoft.Maui.Controls.resources.dll.so => 356
	i64 u0xb0ac21bec8f428c5, ; 768: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android.dll => 285
	i64 u0xb0bb43dc52ea59f9, ; 769: System.Diagnostics.Tracing.dll => 34
	i64 u0xb110d64b6c9fbe46, ; 770: lib_Microsoft.Extensions.Identity.Core.dll.so => 205
	i64 u0xb1ccbf6243328d1c, ; 771: Microsoft.AspNetCore.Components => 176
	i64 u0xb1dd05401aa8ee63, ; 772: System.Security.AccessControl => 117
	i64 u0xb220631954820169, ; 773: System.Text.RegularExpressions => 137
	i64 u0xb2376e1dbf8b4ed7, ; 774: System.Security.Cryptography.Csp => 121
	i64 u0xb2a1959fe95c5402, ; 775: lib_System.Runtime.InteropServices.JavaScript.dll.so => 105
	i64 u0xb2a3f67f3bf29fce, ; 776: da/Microsoft.Maui.Controls.resources => 333
	i64 u0xb3011a0a57f7ffb2, ; 777: Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll => 365
	i64 u0xb3874072ee0ecf8c, ; 778: Xamarin.AndroidX.VectorDrawable.Animated.dll => 309
	i64 u0xb398860d6ed7ba2f, ; 779: System.Security.Cryptography.ProtectedData => 235
	i64 u0xb3f0a0fcda8d3ebc, ; 780: Xamarin.AndroidX.CardView => 253
	i64 u0xb46be1aa6d4fff93, ; 781: hi/Microsoft.Maui.Controls.resources => 340
	i64 u0xb477491be13109d8, ; 782: ar/Microsoft.Maui.Controls.resources => 330
	i64 u0xb4bd7015ecee9d86, ; 783: System.IO.Pipelines => 53
	i64 u0xb4c53d9749c5f226, ; 784: lib_System.IO.FileSystem.AccessControl.dll.so => 46
	i64 u0xb4ff710863453fda, ; 785: System.Diagnostics.FileVersionInfo.dll => 28
	i64 u0xb5c38bf497a4cfe2, ; 786: lib_System.Threading.Tasks.dll.so => 143
	i64 u0xb5c7fcdafbc67ee4, ; 787: Microsoft.Extensions.Logging.Abstractions.dll => 208
	i64 u0xb5ea31d5244c6626, ; 788: System.Threading.ThreadPool.dll => 145
	i64 u0xb66575354464a3ec, ; 789: Xamarin.Kotlin.StdLib.Common => 322
	i64 u0xb7212c4683a94afe, ; 790: System.Drawing.Primitives => 35
	i64 u0xb7b7753d1f319409, ; 791: sv/Microsoft.Maui.Controls.resources => 356
	i64 u0xb81a2c6e0aee50fe, ; 792: lib_System.Private.CoreLib.dll.so => 171
	i64 u0xb8b0a9b3dfbc5cb7, ; 793: Xamarin.AndroidX.Window.Extensions.Core.Core => 314
	i64 u0xb8c60af47c08d4da, ; 794: System.Net.ServicePoint => 74
	i64 u0xb8e68d20aad91196, ; 795: lib_System.Xml.XPath.dll.so => 159
	i64 u0xb9185c33a1643eed, ; 796: Microsoft.CSharp.dll => 1
	i64 u0xb9b8001adf4ed7cc, ; 797: lib_Xamarin.AndroidX.SlidingPaneLayout.dll.so => 303
	i64 u0xb9f64d3b230def68, ; 798: lib-pt-Microsoft.Maui.Controls.resources.dll.so => 352
	i64 u0xb9fc3c8a556e3691, ; 799: ja/Microsoft.Maui.Controls.resources => 345
	i64 u0xba4670aa94a2b3c6, ; 800: lib_System.Xml.XDocument.dll.so => 157
	i64 u0xba48785529705af9, ; 801: System.Collections.dll => 12
	i64 u0xba965b8c86359996, ; 802: lib_System.Windows.dll.so => 153
	i64 u0xbaf762c4825c14e9, ; 803: Microsoft.AspNetCore.Components.WebView => 179
	i64 u0xbb286883bc35db36, ; 804: System.Transactions.dll => 149
	i64 u0xbb65706fde942ce3, ; 805: System.Net.Sockets => 75
	i64 u0xbb8c8d165ef11460, ; 806: lib_Microsoft.Identity.Client.Extensions.Msal.dll.so => 213
	i64 u0xbba28979413cad9e, ; 807: lib_System.Runtime.CompilerServices.VisualC.dll.so => 102
	i64 u0xbbd180354b67271a, ; 808: System.Runtime.Serialization.Formatters => 111
	i64 u0xbc260cdba33291a3, ; 809: Xamarin.AndroidX.Arch.Core.Common.dll => 250
	i64 u0xbc3c4e8dffea9d4e, ; 810: Microsoft.AspNetCore.Metadata.dll => 184
	i64 u0xbcd36316d29f27b4, ; 811: lib_Microsoft.AspNetCore.Authorization.dll.so => 175
	i64 u0xbcfa7c134d2089f3, ; 812: System.Runtime.Caching => 234
	i64 u0xbd0e2c0d55246576, ; 813: System.Net.Http.dll => 64
	i64 u0xbd3fbd85b9e1cb29, ; 814: lib_System.Net.HttpListener.dll.so => 65
	i64 u0xbd437a2cdb333d0d, ; 815: Xamarin.AndroidX.ViewPager2 => 312
	i64 u0xbd4f572d2bd0a789, ; 816: System.IO.Compression.ZipFile.dll => 44
	i64 u0xbd5d0b88d3d647a5, ; 817: lib_Xamarin.AndroidX.Browser.dll.so => 252
	i64 u0xbd877b14d0b56392, ; 818: System.Runtime.Intrinsics.dll => 108
	i64 u0xbe65a49036345cf4, ; 819: lib_System.Buffers.dll.so => 7
	i64 u0xbee1b395605474f1, ; 820: System.Drawing.Common.dll => 230
	i64 u0xbee38d4a88835966, ; 821: Xamarin.AndroidX.AppCompat.AppCompatResources => 249
	i64 u0xbef9919db45b4ca7, ; 822: System.IO.Pipes.AccessControl => 54
	i64 u0xbf0fa68611139208, ; 823: lib_Xamarin.AndroidX.Annotation.dll.so => 245
	i64 u0xbfc1e1fb3095f2b3, ; 824: lib_System.Net.Http.Json.dll.so => 63
	i64 u0xc040a4ab55817f58, ; 825: ar/Microsoft.Maui.Controls.resources.dll => 330
	i64 u0xc07cadab29efeba0, ; 826: Xamarin.AndroidX.Core.Core.Ktx.dll => 262
	i64 u0xc091d2dc9afbf2c3, ; 827: SMITCOMIDAS => 0
	i64 u0xc0d928351ab5ca77, ; 828: System.Console.dll => 20
	i64 u0xc0f5a221a9383aea, ; 829: System.Runtime.Intrinsics => 108
	i64 u0xc111030af54d7191, ; 830: System.Resources.Writer => 100
	i64 u0xc12b8b3afa48329c, ; 831: lib_System.Linq.dll.so => 61
	i64 u0xc183ca0b74453aa9, ; 832: lib_System.Threading.Tasks.Dataflow.dll.so => 140
	i64 u0xc1c2cb7af77b8858, ; 833: Microsoft.EntityFrameworkCore => 187
	i64 u0xc1ebdc7e6a943450, ; 834: Microsoft.AspNetCore.Authorization.dll => 175
	i64 u0xc1ff9ae3cdb6e1e6, ; 835: Xamarin.AndroidX.Activity.dll => 243
	i64 u0xc2654c6e949f22d9, ; 836: Microsoft.AspNetCore.Identity.EntityFrameworkCore.dll => 183
	i64 u0xc26c064effb1dea9, ; 837: System.Buffers.dll => 7
	i64 u0xc278de356ad8a9e3, ; 838: Microsoft.IdentityModel.Logging => 216
	i64 u0xc28c50f32f81cc73, ; 839: ja/Microsoft.Maui.Controls.resources.dll => 345
	i64 u0xc2902f6cf5452577, ; 840: lib_Mono.Android.Export.dll.so => 168
	i64 u0xc2a3bca55b573141, ; 841: System.IO.FileSystem.Watcher => 49
	i64 u0xc2bcfec99f69365e, ; 842: Xamarin.AndroidX.ViewPager2.dll => 312
	i64 u0xc30b52815b58ac2c, ; 843: lib_System.Runtime.Serialization.Xml.dll.so => 114
	i64 u0xc36d7d89c652f455, ; 844: System.Threading.Overlapped => 139
	i64 u0xc396b285e59e5493, ; 845: GoogleGson.dll => 174
	i64 u0xc3c86c1e5e12f03d, ; 846: WindowsBase => 164
	i64 u0xc421b61fd853169d, ; 847: lib_System.Net.WebSockets.Client.dll.so => 79
	i64 u0xc463e077917aa21d, ; 848: System.Runtime.Serialization.Json => 112
	i64 u0xc472ce300460ccb6, ; 849: Microsoft.EntityFrameworkCore.dll => 187
	i64 u0xc4d3858ed4d08512, ; 850: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 289
	i64 u0xc4d69851fe06342f, ; 851: lib_Microsoft.Extensions.Caching.Memory.dll.so => 192
	i64 u0xc50fded0ded1418c, ; 852: lib_System.ComponentModel.TypeConverter.dll.so => 17
	i64 u0xc519125d6bc8fb11, ; 853: lib_System.Net.Requests.dll.so => 72
	i64 u0xc5293b19e4dc230e, ; 854: Xamarin.AndroidX.Navigation.Fragment => 293
	i64 u0xc5325b2fcb37446f, ; 855: lib_System.Private.Xml.dll.so => 88
	i64 u0xc535cb9a21385d9b, ; 856: lib_Xamarin.Android.Glide.DiskLruCache.dll.so => 241
	i64 u0xc5a0f4b95a699af7, ; 857: lib_System.Private.Uri.dll.so => 86
	i64 u0xc5cdcd5b6277579e, ; 858: lib_System.Security.Cryptography.Algorithms.dll.so => 119
	i64 u0xc5ec286825cb0bf4, ; 859: Xamarin.AndroidX.Tracing.Tracing => 306
	i64 u0xc659b586d4c229e2, ; 860: Microsoft.Extensions.Configuration.FileExtensions.dll => 196
	i64 u0xc6706bc8aa7fe265, ; 861: Xamarin.AndroidX.Annotation.Jvm => 247
	i64 u0xc7c01e7d7c93a110, ; 862: System.Text.Encoding.Extensions.dll => 134
	i64 u0xc7ce851898a4548e, ; 863: lib_System.Web.HttpUtility.dll.so => 151
	i64 u0xc809d4089d2556b2, ; 864: System.Runtime.InteropServices.JavaScript.dll => 105
	i64 u0xc858a28d9ee5a6c5, ; 865: lib_System.Collections.Specialized.dll.so => 11
	i64 u0xc8ac7c6bf1c2ec51, ; 866: System.Reflection.DispatchProxy.dll => 89
	i64 u0xc99ccc413e3ce0d4, ; 867: lib_Microsoft.AspNetCore.Identity.EntityFrameworkCore.dll.so => 183
	i64 u0xc9c62c8f354ac568, ; 868: lib_System.Diagnostics.TextWriterTraceListener.dll.so => 31
	i64 u0xca3110fea81c8916, ; 869: Microsoft.AspNetCore.Components.Web.dll => 178
	i64 u0xca32340d8d54dcd5, ; 870: Microsoft.Extensions.Caching.Memory.dll => 192
	i64 u0xca3a723e7342c5b6, ; 871: lib-tr-Microsoft.Maui.Controls.resources.dll.so => 358
	i64 u0xca5801070d9fccfb, ; 872: System.Text.Encoding => 135
	i64 u0xcab3493c70141c2d, ; 873: pl/Microsoft.Maui.Controls.resources => 350
	i64 u0xcacfddc9f7c6de76, ; 874: ro/Microsoft.Maui.Controls.resources.dll => 353
	i64 u0xcadbc92899a777f0, ; 875: Xamarin.AndroidX.Startup.StartupRuntime => 304
	i64 u0xcb45618372c47127, ; 876: Microsoft.EntityFrameworkCore.Relational => 189
	i64 u0xcba1cb79f45292b5, ; 877: Xamarin.Android.Glide.GifDecoder.dll => 242
	i64 u0xcbb5f80c7293e696, ; 878: lib_System.Globalization.Calendars.dll.so => 39
	i64 u0xcbd4fdd9cef4a294, ; 879: lib__Microsoft.Android.Resource.Designer.dll.so => 368
	i64 u0xcc15da1e07bbd994, ; 880: Xamarin.AndroidX.SlidingPaneLayout => 303
	i64 u0xcc182c3afdc374d6, ; 881: Microsoft.Bcl.AsyncInterfaces => 185
	i64 u0xcc2876b32ef2794c, ; 882: lib_System.Text.RegularExpressions.dll.so => 137
	i64 u0xcc5c3bb714c4561e, ; 883: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 327
	i64 u0xcc76886e09b88260, ; 884: Xamarin.KotlinX.Serialization.Core.Jvm.dll => 329
	i64 u0xcc9fa2923aa1c9ef, ; 885: System.Diagnostics.Contracts.dll => 25
	i64 u0xccf25c4b634ccd3a, ; 886: zh-Hans/Microsoft.Maui.Controls.resources.dll => 362
	i64 u0xcd10a42808629144, ; 887: System.Net.Requests => 72
	i64 u0xcd3586b93136841e, ; 888: lib_System.Runtime.Caching.dll.so => 234
	i64 u0xcdca1b920e9f53ba, ; 889: Xamarin.AndroidX.Interpolator => 274
	i64 u0xcdd0c48b6937b21c, ; 890: Xamarin.AndroidX.SwipeRefreshLayout => 305
	i64 u0xceb28d385f84f441, ; 891: Azure.Core.dll => 172
	i64 u0xcf140ed700bc8e66, ; 892: Microsoft.SqlServer.Server.dll => 226
	i64 u0xcf23d8093f3ceadf, ; 893: System.Diagnostics.DiagnosticSource.dll => 27
	i64 u0xcf5ff6b6b2c4c382, ; 894: System.Net.Mail.dll => 66
	i64 u0xcf8fc898f98b0d34, ; 895: System.Private.Xml.Linq => 87
	i64 u0xd04b5f59ed596e31, ; 896: System.Reflection.Metadata.dll => 94
	i64 u0xd063299fcfc0c93f, ; 897: lib_System.Runtime.Serialization.Json.dll.so => 112
	i64 u0xd0de8a113e976700, ; 898: System.Diagnostics.TextWriterTraceListener => 31
	i64 u0xd0fc33d5ae5d4cb8, ; 899: System.Runtime.Extensions => 103
	i64 u0xd1194e1d8a8de83c, ; 900: lib_Xamarin.AndroidX.Lifecycle.Common.Jvm.dll.so => 277
	i64 u0xd12beacdfc14f696, ; 901: System.Dynamic.Runtime => 37
	i64 u0xd198e7ce1b6a8344, ; 902: System.Net.Quic.dll => 71
	i64 u0xd22a0c4630f2fe66, ; 903: lib_System.Security.Cryptography.ProtectedData.dll.so => 235
	i64 u0xd2505d8abeed6983, ; 904: lib_Microsoft.AspNetCore.Components.Web.dll.so => 178
	i64 u0xd3144156a3727ebe, ; 905: Xamarin.Google.Guava.ListenableFuture => 319
	i64 u0xd333d0af9e423810, ; 906: System.Runtime.InteropServices => 107
	i64 u0xd33a415cb4278969, ; 907: System.Security.Cryptography.Encoding.dll => 122
	i64 u0xd3426d966bb704f5, ; 908: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 249
	i64 u0xd3651b6fc3125825, ; 909: System.Private.Uri.dll => 86
	i64 u0xd373685349b1fe8b, ; 910: Microsoft.Extensions.Logging.dll => 207
	i64 u0xd3801faafafb7698, ; 911: System.Private.DataContractSerialization.dll => 85
	i64 u0xd3e4c8d6a2d5d470, ; 912: it/Microsoft.Maui.Controls.resources => 344
	i64 u0xd3edcc1f25459a50, ; 913: System.Reflection.Emit => 92
	i64 u0xd42655883bb8c19f, ; 914: Microsoft.EntityFrameworkCore.Abstractions.dll => 188
	i64 u0xd4645626dffec99d, ; 915: lib_Microsoft.Extensions.DependencyInjection.Abstractions.dll.so => 199
	i64 u0xd46b4a8758d1f3ee, ; 916: Microsoft.Extensions.FileProviders.Composite.dll => 201
	i64 u0xd4fa0abb79079ea9, ; 917: System.Security.Principal.dll => 128
	i64 u0xd5507e11a2b2839f, ; 918: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 289
	i64 u0xd5d04bef8478ea19, ; 919: Xamarin.AndroidX.Tracing.Tracing.dll => 306
	i64 u0xd60815f26a12e140, ; 920: Microsoft.Extensions.Logging.Debug.dll => 209
	i64 u0xd65786d27a4ad960, ; 921: lib_Microsoft.Maui.Controls.HotReload.Forms.dll.so => 364
	i64 u0xd6694f8359737e4e, ; 922: Xamarin.AndroidX.SavedState => 300
	i64 u0xd6949e129339eae5, ; 923: lib_Xamarin.AndroidX.Core.Core.Ktx.dll.so => 262
	i64 u0xd6d21782156bc35b, ; 924: Xamarin.AndroidX.SwipeRefreshLayout.dll => 305
	i64 u0xd6de019f6af72435, ; 925: Xamarin.AndroidX.ConstraintLayout.Core.dll => 259
	i64 u0xd70956d1e6deefb9, ; 926: Jsr305Binding => 316
	i64 u0xd72329819cbbbc44, ; 927: lib_Microsoft.Extensions.Configuration.Abstractions.dll.so => 194
	i64 u0xd72c760af136e863, ; 928: System.Xml.XmlSerializer.dll => 161
	i64 u0xd753f071e44c2a03, ; 929: lib_System.Security.SecureString.dll.so => 129
	i64 u0xd7b3764ada9d341d, ; 930: lib_Microsoft.Extensions.Logging.Abstractions.dll.so => 208
	i64 u0xd7f0088bc5ad71f2, ; 931: Xamarin.AndroidX.VersionedParcelable => 310
	i64 u0xd8fb25e28ae30a12, ; 932: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 297
	i64 u0xda1dfa4c534a9251, ; 933: Microsoft.Extensions.DependencyInjection => 198
	i64 u0xdad05a11827959a3, ; 934: System.Collections.NonGeneric.dll => 10
	i64 u0xdaefdfe71aa53cf9, ; 935: System.IO.FileSystem.Primitives => 48
	i64 u0xdb5383ab5865c007, ; 936: lib-vi-Microsoft.Maui.Controls.resources.dll.so => 360
	i64 u0xdb58816721c02a59, ; 937: lib_System.Reflection.Emit.ILGeneration.dll.so => 90
	i64 u0xdb9f2880a64da6d6, ; 938: Microsoft.Extensions.Identity.Stores.dll => 206
	i64 u0xdbeda89f832aa805, ; 939: vi/Microsoft.Maui.Controls.resources.dll => 360
	i64 u0xdbf2a779fbc3ac31, ; 940: System.Transactions.Local.dll => 148
	i64 u0xdbf9607a441b4505, ; 941: System.Linq => 61
	i64 u0xdbfc90157a0de9b0, ; 942: lib_System.Text.Encoding.dll.so => 135
	i64 u0xdc75032002d1a212, ; 943: lib_System.Transactions.Local.dll.so => 148
	i64 u0xdca8be7403f92d4f, ; 944: lib_System.Linq.Queryable.dll.so => 60
	i64 u0xdce2c53525640bf3, ; 945: Microsoft.Extensions.Logging => 207
	i64 u0xdd2b722d78ef5f43, ; 946: System.Runtime.dll => 116
	i64 u0xdd67031857c72f96, ; 947: lib_System.Text.Encodings.Web.dll.so => 136
	i64 u0xdd92e229ad292030, ; 948: System.Numerics.dll => 83
	i64 u0xdddcdd701e911af1, ; 949: lib_Xamarin.AndroidX.Legacy.Support.Core.Utils.dll.so => 275
	i64 u0xdde30e6b77aa6f6c, ; 950: lib-zh-Hans-Microsoft.Maui.Controls.resources.dll.so => 362
	i64 u0xde110ae80fa7c2e2, ; 951: System.Xml.XDocument.dll => 157
	i64 u0xde4726fcdf63a198, ; 952: Xamarin.AndroidX.Transition => 307
	i64 u0xde572c2b2fb32f93, ; 953: lib_System.Threading.Tasks.Extensions.dll.so => 141
	i64 u0xde8769ebda7d8647, ; 954: hr/Microsoft.Maui.Controls.resources.dll => 341
	i64 u0xdee075f3477ef6be, ; 955: Xamarin.AndroidX.ExifInterface.dll => 271
	i64 u0xdf4b773de8fb1540, ; 956: System.Net.dll => 81
	i64 u0xdfa254ebb4346068, ; 957: System.Net.Ping => 69
	i64 u0xe0142572c095a480, ; 958: Xamarin.AndroidX.AppCompat.dll => 248
	i64 u0xe021eaa401792a05, ; 959: System.Text.Encoding.dll => 135
	i64 u0xe02f89350ec78051, ; 960: Xamarin.AndroidX.CoordinatorLayout.dll => 260
	i64 u0xe0496b9d65ef5474, ; 961: Xamarin.Android.Glide.DiskLruCache.dll => 241
	i64 u0xe0be470debe77c12, ; 962: Microsoft.AspNetCore.Cryptography.Internal.dll => 181
	i64 u0xe10b760bb1462e7a, ; 963: lib_System.Security.Cryptography.Primitives.dll.so => 124
	i64 u0xe1566bbdb759c5af, ; 964: Microsoft.Maui.Controls.HotReload.Forms.dll => 364
	i64 u0xe192a588d4410686, ; 965: lib_System.IO.Pipelines.dll.so => 53
	i64 u0xe1a08bd3fa539e0d, ; 966: System.Runtime.Loader => 109
	i64 u0xe1a77eb8831f7741, ; 967: System.Security.SecureString.dll => 129
	i64 u0xe1b52f9f816c70ef, ; 968: System.Private.Xml.Linq.dll => 87
	i64 u0xe1e199c8ab02e356, ; 969: System.Data.DataSetExtensions.dll => 23
	i64 u0xe1ecfdb7fff86067, ; 970: System.Net.Security.dll => 73
	i64 u0xe2252a80fe853de4, ; 971: lib_System.Security.Principal.dll.so => 128
	i64 u0xe22fa4c9c645db62, ; 972: System.Diagnostics.TextWriterTraceListener.dll => 31
	i64 u0xe2420585aeceb728, ; 973: System.Net.Requests.dll => 72
	i64 u0xe26692647e6bcb62, ; 974: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 284
	i64 u0xe29b73bc11392966, ; 975: lib-id-Microsoft.Maui.Controls.resources.dll.so => 343
	i64 u0xe2ad448dee50fbdf, ; 976: System.Xml.Serialization => 156
	i64 u0xe2d920f978f5d85c, ; 977: System.Data.DataSetExtensions => 23
	i64 u0xe2e426c7714fa0bc, ; 978: Microsoft.Win32.Primitives.dll => 4
	i64 u0xe31089e70e4e84ee, ; 979: Microsoft.AspNetCore.Components.WebView.Maui => 180
	i64 u0xe332bacb3eb4a806, ; 980: Mono.Android.Export.dll => 168
	i64 u0xe3811d68d4fe8463, ; 981: pt-BR/Microsoft.Maui.Controls.resources.dll => 351
	i64 u0xe3b7cbae5ad66c75, ; 982: lib_System.Security.Cryptography.Encoding.dll.so => 122
	i64 u0xe494f7ced4ecd10a, ; 983: hu/Microsoft.Maui.Controls.resources.dll => 342
	i64 u0xe4a9b1e40d1e8917, ; 984: lib-fi-Microsoft.Maui.Controls.resources.dll.so => 337
	i64 u0xe4f74a0b5bf9703f, ; 985: System.Runtime.Serialization.Primitives => 113
	i64 u0xe5434e8a119ceb69, ; 986: lib_Mono.Android.dll.so => 170
	i64 u0xe55703b9ce5c038a, ; 987: System.Diagnostics.Tools => 32
	i64 u0xe57013c8afc270b5, ; 988: Microsoft.VisualBasic => 3
	i64 u0xe57d22ca4aeb4900, ; 989: System.Configuration.ConfigurationManager => 229
	i64 u0xe62913cc36bc07ec, ; 990: System.Xml.dll => 162
	i64 u0xe6fef862bc2c39f6, ; 991: SMITCOMIDAS.Shared => 367
	i64 u0xe79d45aa815dab7f, ; 992: System.Runtime.Caching.dll => 234
	i64 u0xe7bea09c4900a191, ; 993: Xamarin.AndroidX.VectorDrawable.dll => 308
	i64 u0xe7e03cc18dcdeb49, ; 994: lib_System.Diagnostics.StackTrace.dll.so => 30
	i64 u0xe7e147ff99a7a380, ; 995: lib_System.Configuration.dll.so => 19
	i64 u0xe86b0df4ba9e5db8, ; 996: lib_Xamarin.AndroidX.Lifecycle.Runtime.Android.dll.so => 283
	i64 u0xe896622fe0902957, ; 997: System.Reflection.Emit.dll => 92
	i64 u0xe89a2a9ef110899b, ; 998: System.Drawing.dll => 36
	i64 u0xe8c35a466559994c, ; 999: lib_Microsoft.Extensions.Identity.Stores.dll.so => 206
	i64 u0xe8c5f8c100b5934b, ; 1000: Microsoft.Win32.Registry => 5
	i64 u0xe957c3976986ab72, ; 1001: lib_Xamarin.AndroidX.Window.Extensions.Core.Core.dll.so => 314
	i64 u0xe9772100456fb4b4, ; 1002: Microsoft.AspNetCore.Components.dll => 176
	i64 u0xe98163eb702ae5c5, ; 1003: Xamarin.AndroidX.Arch.Core.Runtime => 251
	i64 u0xe994f23ba4c143e5, ; 1004: Xamarin.KotlinX.Coroutines.Android => 325
	i64 u0xe9b9c8c0458fd92a, ; 1005: System.Windows => 153
	i64 u0xe9d166d87a7f2bdb, ; 1006: lib_Xamarin.AndroidX.Startup.StartupRuntime.dll.so => 304
	i64 u0xea154e342c6ac70f, ; 1007: Microsoft.Extensions.FileProviders.Embedded.dll => 202
	i64 u0xea5a4efc2ad81d1b, ; 1008: Xamarin.Google.ErrorProne.Annotations => 318
	i64 u0xeb2313fe9d65b785, ; 1009: Xamarin.AndroidX.ConstraintLayout.dll => 258
	i64 u0xeb6e275e78cb8d42, ; 1010: Xamarin.AndroidX.LocalBroadcastManager.dll => 291
	i64 u0xeb9e30ac32aac03e, ; 1011: lib_Microsoft.Win32.SystemEvents.dll.so => 227
	i64 u0xebc05bf326a78ad3, ; 1012: System.Windows.Extensions.dll => 238
	i64 u0xec8abb68d340aac6, ; 1013: Microsoft.AspNetCore.Authorization => 175
	i64 u0xed19c616b3fcb7eb, ; 1014: Xamarin.AndroidX.VersionedParcelable.dll => 310
	i64 u0xed60c6fa891c051a, ; 1015: lib_Microsoft.VisualStudio.DesignTools.TapContract.dll.so => 366
	i64 u0xedc4817167106c23, ; 1016: System.Net.Sockets.dll => 75
	i64 u0xedc632067fb20ff3, ; 1017: System.Memory.dll => 62
	i64 u0xedc8e4ca71a02a8b, ; 1018: Xamarin.AndroidX.Navigation.Runtime.dll => 294
	i64 u0xee81f5b3f1c4f83b, ; 1019: System.Threading.ThreadPool => 145
	i64 u0xeeb7ebb80150501b, ; 1020: lib_Xamarin.AndroidX.Collection.Jvm.dll.so => 255
	i64 u0xeefc635595ef57f0, ; 1021: System.Security.Cryptography.Cng => 120
	i64 u0xef03b1b5a04e9709, ; 1022: System.Text.Encoding.CodePages.dll => 133
	i64 u0xef432781d5667f61, ; 1023: Xamarin.AndroidX.Print => 296
	i64 u0xef602c523fe2e87a, ; 1024: lib_Xamarin.Google.Guava.ListenableFuture.dll.so => 319
	i64 u0xef72742e1bcca27a, ; 1025: Microsoft.Maui.Essentials.dll => 224
	i64 u0xefd1e0c4e5c9b371, ; 1026: System.Resources.ResourceManager.dll => 99
	i64 u0xefe8f8d5ed3c72ea, ; 1027: System.Formats.Tar.dll => 38
	i64 u0xefec0b7fdc57ec42, ; 1028: Xamarin.AndroidX.Activity => 243
	i64 u0xf00c29406ea45e19, ; 1029: es/Microsoft.Maui.Controls.resources.dll => 336
	i64 u0xf09e47b6ae914f6e, ; 1030: System.Net.NameResolution => 67
	i64 u0xf0ac2b489fed2e35, ; 1031: lib_System.Diagnostics.Debug.dll.so => 26
	i64 u0xf0bb49dadd3a1fe1, ; 1032: lib_System.Net.ServicePoint.dll.so => 74
	i64 u0xf0de2537ee19c6ca, ; 1033: lib_System.Net.WebHeaderCollection.dll.so => 77
	i64 u0xf1138779fa181c68, ; 1034: lib_Xamarin.AndroidX.Lifecycle.Runtime.dll.so => 282
	i64 u0xf11b621fc87b983f, ; 1035: Microsoft.Maui.Controls.Xaml.dll => 222
	i64 u0xf161f4f3c3b7e62c, ; 1036: System.Data => 24
	i64 u0xf16eb650d5a464bc, ; 1037: System.ValueTuple => 150
	i64 u0xf1c4b4005493d871, ; 1038: System.Formats.Asn1.dll => 231
	i64 u0xf238bd79489d3a96, ; 1039: lib-nl-Microsoft.Maui.Controls.resources.dll.so => 349
	i64 u0xf2feea356ba760af, ; 1040: Xamarin.AndroidX.Arch.Core.Runtime.dll => 251
	i64 u0xf300e085f8acd238, ; 1041: lib_System.ServiceProcess.dll.so => 132
	i64 u0xf34e52b26e7e059d, ; 1042: System.Runtime.CompilerServices.VisualC.dll => 102
	i64 u0xf37221fda4ef8830, ; 1043: lib_Xamarin.Google.Android.Material.dll.so => 315
	i64 u0xf3ad9b8fb3eefd12, ; 1044: lib_System.IO.UnmanagedMemoryStream.dll.so => 56
	i64 u0xf3ddfe05336abf29, ; 1045: System => 163
	i64 u0xf408654b2a135055, ; 1046: System.Reflection.Emit.ILGeneration.dll => 90
	i64 u0xf4103170a1de5bd0, ; 1047: System.Linq.Queryable.dll => 60
	i64 u0xf42d20c23173d77c, ; 1048: lib_System.ServiceModel.Web.dll.so => 131
	i64 u0xf4c1dd70a5496a17, ; 1049: System.IO.Compression => 45
	i64 u0xf4ecf4b9afc64781, ; 1050: System.ServiceProcess.dll => 132
	i64 u0xf4eeeaa566e9b970, ; 1051: lib_Xamarin.AndroidX.CustomView.PoolingContainer.dll.so => 265
	i64 u0xf518f63ead11fcd1, ; 1052: System.Threading.Tasks => 143
	i64 u0xf5e59d7ac34b50aa, ; 1053: Microsoft.IdentityModel.Protocols.dll => 217
	i64 u0xf5fc7602fe27b333, ; 1054: System.Net.WebHeaderCollection => 77
	i64 u0xf6077741019d7428, ; 1055: Xamarin.AndroidX.CoordinatorLayout => 260
	i64 u0xf61ade9836ad4692, ; 1056: Microsoft.IdentityModel.Tokens.dll => 219
	i64 u0xf6742cbf457c450b, ; 1057: Xamarin.AndroidX.Lifecycle.Runtime.Android.dll => 283
	i64 u0xf6c0e7d55a7a4e4f, ; 1058: Microsoft.IdentityModel.JsonWebTokens => 215
	i64 u0xf6ca47f59775de8c, ; 1059: SMITCOMIDAS.Shared.dll => 367
	i64 u0xf6de7fa3776f8927, ; 1060: lib_Microsoft.Extensions.Configuration.Json.dll.so => 197
	i64 u0xf70c0a7bf8ccf5af, ; 1061: System.Web => 152
	i64 u0xf77b20923f07c667, ; 1062: de/Microsoft.Maui.Controls.resources.dll => 334
	i64 u0xf7be38c7938ad857, ; 1063: Microsoft.AspNetCore.Cryptography.KeyDerivation => 182
	i64 u0xf7e2cac4c45067b3, ; 1064: lib_System.Numerics.Vectors.dll.so => 82
	i64 u0xf7e74930e0e3d214, ; 1065: zh-HK/Microsoft.Maui.Controls.resources.dll => 361
	i64 u0xf84773b5c81e3cef, ; 1066: lib-uk-Microsoft.Maui.Controls.resources.dll.so => 359
	i64 u0xf8aac5ea82de1348, ; 1067: System.Linq.Queryable => 60
	i64 u0xf8b77539b362d3ba, ; 1068: lib_System.Reflection.Primitives.dll.so => 95
	i64 u0xf8e045dc345b2ea3, ; 1069: lib_Xamarin.AndroidX.RecyclerView.dll.so => 298
	i64 u0xf915dc29808193a1, ; 1070: System.Web.HttpUtility.dll => 151
	i64 u0xf96c777a2a0686f4, ; 1071: hi/Microsoft.Maui.Controls.resources.dll => 340
	i64 u0xf9be54c8bcf8ff3b, ; 1072: System.Security.AccessControl.dll => 117
	i64 u0xf9eec5bb3a6aedc6, ; 1073: Microsoft.Extensions.Options => 210
	i64 u0xfa0e82300e67f913, ; 1074: lib_System.AppContext.dll.so => 6
	i64 u0xfa2fdb27e8a2c8e8, ; 1075: System.ComponentModel.EventBasedAsync => 15
	i64 u0xfa3f278f288b0e84, ; 1076: lib_System.Net.Security.dll.so => 73
	i64 u0xfa504dfa0f097d72, ; 1077: Microsoft.Extensions.FileProviders.Abstractions.dll => 200
	i64 u0xfa5ed7226d978949, ; 1078: lib-ar-Microsoft.Maui.Controls.resources.dll.so => 330
	i64 u0xfa645d91e9fc4cba, ; 1079: System.Threading.Thread => 144
	i64 u0xfad4d2c770e827f9, ; 1080: lib_System.IO.IsolatedStorage.dll.so => 51
	i64 u0xfb06dd2338e6f7c4, ; 1081: System.Net.Ping.dll => 69
	i64 u0xfb087abe5365e3b7, ; 1082: lib_System.Data.DataSetExtensions.dll.so => 23
	i64 u0xfb846e949baff5ea, ; 1083: System.Xml.Serialization.dll => 156
	i64 u0xfbad3e4ce4b98145, ; 1084: System.Security.Cryptography.X509Certificates => 125
	i64 u0xfbf0a31c9fc34bc4, ; 1085: lib_System.Net.Http.dll.so => 64
	i64 u0xfc61ddcf78dd1f54, ; 1086: Xamarin.AndroidX.LocalBroadcastManager => 291
	i64 u0xfc6b7527cc280b3f, ; 1087: lib_System.Runtime.Serialization.Formatters.dll.so => 111
	i64 u0xfc719aec26adf9d9, ; 1088: Xamarin.AndroidX.Navigation.Fragment.dll => 293
	i64 u0xfc82690c2fe2735c, ; 1089: Xamarin.AndroidX.Lifecycle.Process.dll => 281
	i64 u0xfc93fc307d279893, ; 1090: System.IO.Pipes.AccessControl.dll => 54
	i64 u0xfcd302092ada6328, ; 1091: System.IO.MemoryMappedFiles.dll => 52
	i64 u0xfd22f00870e40ae0, ; 1092: lib_Xamarin.AndroidX.DrawerLayout.dll.so => 267
	i64 u0xfd2e866c678cac90, ; 1093: lib_Microsoft.AspNetCore.Components.WebView.Maui.dll.so => 180
	i64 u0xfd49b3c1a76e2748, ; 1094: System.Runtime.InteropServices.RuntimeInformation => 106
	i64 u0xfd536c702f64dc47, ; 1095: System.Text.Encoding.Extensions => 134
	i64 u0xfd583f7657b6a1cb, ; 1096: Xamarin.AndroidX.Fragment => 272
	i64 u0xfd8dd91a2c26bd5d, ; 1097: Xamarin.AndroidX.Lifecycle.Runtime => 282
	i64 u0xfda36abccf05cf5c, ; 1098: System.Net.WebSockets.Client => 79
	i64 u0xfddbe9695626a7f5, ; 1099: Xamarin.AndroidX.Lifecycle.Common => 276
	i64 u0xfe9856c3af9365ab, ; 1100: lib_Microsoft.Extensions.Configuration.FileExtensions.dll.so => 196
	i64 u0xfeae9952cf03b8cb, ; 1101: tr/Microsoft.Maui.Controls.resources => 358
	i64 u0xfebe1950717515f9, ; 1102: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 280
	i64 u0xff270a55858bac8d, ; 1103: System.Security.Principal => 128
	i64 u0xff9b54613e0d2cc8, ; 1104: System.Net.Http.Json => 63
	i64 u0xffdb7a971be4ec73, ; 1105: System.ValueTuple.dll => 150
	i64 u0xfff40914e0b38d3d ; 1106: Azure.Identity.dll => 173
], align 16

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [1107 x i32] [
	i32 41, i32 326, i32 305, i32 13, i32 294, i32 104, i32 192, i32 169,
	i32 47, i32 248, i32 7, i32 85, i32 354, i32 332, i32 360, i32 214,
	i32 268, i32 322, i32 70, i32 298, i32 12, i32 223, i32 101, i32 361,
	i32 154, i32 19, i32 273, i32 255, i32 176, i32 159, i32 270, i32 308,
	i32 165, i32 354, i32 10, i32 209, i32 309, i32 172, i32 95, i32 265,
	i32 267, i32 13, i32 210, i32 10, i32 233, i32 126, i32 94, i32 191,
	i32 138, i32 186, i32 38, i32 355, i32 329, i32 182, i32 311, i32 351,
	i32 170, i32 242, i32 5, i32 224, i32 66, i32 302, i32 129, i32 205,
	i32 185, i32 301, i32 269, i32 67, i32 256, i32 65, i32 56, i32 185,
	i32 264, i32 51, i32 42, i32 124, i32 66, i32 80, i32 284, i32 366,
	i32 156, i32 91, i32 98, i32 298, i32 218, i32 139, i32 149, i32 230,
	i32 252, i32 338, i32 160, i32 167, i32 339, i32 218, i32 199, i32 80,
	i32 366, i32 256, i32 4, i32 5, i32 50, i32 100, i32 55, i32 119,
	i32 97, i32 166, i32 117, i32 326, i32 21, i32 342, i32 136, i32 96,
	i32 329, i32 76, i32 348, i32 296, i32 304, i32 118, i32 173, i32 8,
	i32 163, i32 357, i32 69, i32 241, i32 177, i32 285, i32 299, i32 203,
	i32 169, i32 143, i32 39, i32 302, i32 46, i32 30, i32 295, i32 346,
	i32 142, i32 210, i32 161, i32 205, i32 28, i32 83, i32 306, i32 76,
	i32 42, i32 236, i32 29, i32 41, i32 102, i32 116, i32 246, i32 228,
	i32 44, i32 90, i32 357, i32 55, i32 146, i32 365, i32 144, i32 187,
	i32 99, i32 48, i32 20, i32 261, i32 113, i32 239, i32 338, i32 317,
	i32 367, i32 321, i32 211, i32 93, i32 57, i32 232, i32 343, i32 341,
	i32 80, i32 317, i32 167, i32 26, i32 70, i32 297, i32 271, i32 0,
	i32 364, i32 359, i32 68, i32 33, i32 238, i32 337, i32 14, i32 137,
	i32 232, i32 231, i32 363, i32 204, i32 257, i32 350, i32 133, i32 91,
	i32 87, i32 147, i32 356, i32 24, i32 237, i32 56, i32 50, i32 335,
	i32 220, i32 29, i32 155, i32 226, i32 34, i32 162, i32 191, i32 272,
	i32 214, i32 51, i32 202, i32 368, i32 313, i32 89, i32 253, i32 35,
	i32 338, i32 155, i32 204, i32 9, i32 336, i32 75, i32 226, i32 54,
	i32 201, i32 223, i32 332, i32 221, i32 13, i32 312, i32 193, i32 250,
	i32 108, i32 288, i32 32, i32 103, i32 83, i32 91, i32 52, i32 95,
	i32 320, i32 57, i32 181, i32 9, i32 101, i32 264, i32 67, i32 217,
	i32 229, i32 311, i32 331, i32 200, i32 124, i32 299, i32 115, i32 134,
	i32 220, i32 219, i32 125, i32 105, i32 186, i32 321, i32 130, i32 252,
	i32 319, i32 145, i32 154, i32 273, i32 261, i32 196, i32 268, i32 299,
	i32 96, i32 24, i32 179, i32 303, i32 213, i32 141, i32 296, i32 292,
	i32 173, i32 3, i32 229, i32 165, i32 249, i32 99, i32 159, i32 98,
	i32 25, i32 92, i32 166, i32 170, i32 244, i32 3, i32 350, i32 270,
	i32 1, i32 113, i32 321, i32 188, i32 273, i32 281, i32 232, i32 33,
	i32 6, i32 354, i32 154, i32 233, i32 352, i32 52, i32 275, i32 236,
	i32 84, i32 310, i32 295, i32 43, i32 280, i32 103, i32 46, i32 237,
	i32 228, i32 63, i32 189, i32 290, i32 68, i32 79, i32 58, i32 88,
	i32 152, i32 228, i32 250, i32 132, i32 109, i32 344, i32 290, i32 220,
	i32 297, i32 169, i32 206, i32 133, i32 138, i32 39, i32 331, i32 195,
	i32 219, i32 221, i32 59, i32 195, i32 287, i32 78, i32 25, i32 36,
	i32 98, i32 284, i32 70, i32 22, i32 261, i32 225, i32 181, i32 355,
	i32 120, i32 68, i32 106, i32 361, i32 291, i32 118, i32 116, i32 276,
	i32 277, i32 11, i32 2, i32 123, i32 114, i32 140, i32 40, i32 86,
	i32 245, i32 171, i32 27, i32 146, i32 195, i32 345, i32 198, i32 318,
	i32 244, i32 1, i32 246, i32 233, i32 43, i32 260, i32 147, i32 275,
	i32 18, i32 85, i32 333, i32 40, i32 280, i32 254, i32 285, i32 93,
	i32 207, i32 28, i32 40, i32 77, i32 180, i32 269, i32 257, i32 142,
	i32 107, i32 255, i32 11, i32 104, i32 136, i32 16, i32 121, i32 65,
	i32 155, i32 22, i32 335, i32 328, i32 101, i32 198, i32 327, i32 62,
	i32 57, i32 222, i32 334, i32 109, i32 171, i32 325, i32 9, i32 315,
	i32 119, i32 97, i32 104, i32 288, i32 221, i32 110, i32 247, i32 48,
	i32 20, i32 287, i32 230, i32 263, i32 71, i32 259, i32 153, i32 38,
	i32 333, i32 184, i32 35, i32 323, i32 231, i32 339, i32 314, i32 107,
	i32 348, i32 21, i32 320, i32 190, i32 286, i32 235, i32 225, i32 15,
	i32 211, i32 78, i32 78, i32 263, i32 211, i32 266, i32 293, i32 301,
	i32 150, i32 21, i32 223, i32 332, i32 49, i32 50, i32 322, i32 358,
	i32 348, i32 93, i32 240, i32 344, i32 16, i32 122, i32 341, i32 158,
	i32 44, i32 318, i32 174, i32 115, i32 62, i32 164, i32 183, i32 193,
	i32 14, i32 300, i32 110, i32 247, i32 59, i32 324, i32 197, i32 120,
	i32 347, i32 2, i32 357, i32 236, i32 272, i32 286, i32 227, i32 323,
	i32 179, i32 286, i32 6, i32 254, i32 337, i32 268, i32 215, i32 17,
	i32 355, i32 334, i32 76, i32 258, i32 184, i32 130, i32 320, i32 347,
	i32 177, i32 82, i32 209, i32 12, i32 34, i32 118, i32 328, i32 281,
	i32 270, i32 84, i32 239, i32 182, i32 18, i32 311, i32 194, i32 212,
	i32 279, i32 71, i32 365, i32 94, i32 178, i32 163, i32 274, i32 81,
	i32 363, i32 248, i32 253, i32 324, i32 152, i32 36, i32 149, i32 359,
	i32 214, i32 362, i32 201, i32 142, i32 55, i32 112, i32 189, i32 254,
	i32 308, i32 307, i32 37, i32 363, i32 193, i32 114, i32 204, i32 246,
	i32 14, i32 240, i32 188, i32 144, i32 42, i32 224, i32 244, i32 97,
	i32 327, i32 166, i32 16, i32 47, i32 106, i32 96, i32 191, i32 290,
	i32 27, i32 127, i32 29, i32 339, i32 227, i32 172, i32 200, i32 301,
	i32 127, i32 43, i32 190, i32 263, i32 269, i32 147, i32 8, i32 212,
	i32 292, i32 340, i32 353, i32 352, i32 131, i32 351, i32 41, i32 328,
	i32 33, i32 368, i32 45, i32 141, i32 287, i32 222, i32 278, i32 264,
	i32 197, i32 237, i32 61, i32 131, i32 331, i32 47, i32 158, i32 251,
	i32 278, i32 240, i32 276, i32 347, i32 307, i32 45, i32 162, i32 216,
	i32 274, i32 218, i32 216, i32 336, i32 271, i32 213, i32 343, i32 225,
	i32 18, i32 8, i32 174, i32 262, i32 123, i32 58, i32 139, i32 294,
	i32 346, i32 282, i32 217, i32 316, i32 313, i32 148, i32 140, i32 326,
	i32 323, i32 125, i32 325, i32 158, i32 160, i32 265, i32 243, i32 194,
	i32 349, i32 26, i32 292, i32 279, i32 203, i32 212, i32 81, i32 313,
	i32 126, i32 317, i32 100, i32 146, i32 315, i32 295, i32 53, i32 160,
	i32 165, i32 130, i32 202, i32 37, i32 309, i32 346, i32 177, i32 22,
	i32 190, i32 111, i32 89, i32 266, i32 49, i32 59, i32 121, i32 82,
	i32 126, i32 161, i32 316, i32 164, i32 300, i32 302, i32 267, i32 239,
	i32 186, i32 283, i32 4, i32 277, i32 342, i32 168, i32 2, i32 203,
	i32 288, i32 115, i32 215, i32 245, i32 0, i32 19, i32 208, i32 88,
	i32 64, i32 238, i32 30, i32 199, i32 335, i32 259, i32 58, i32 110,
	i32 279, i32 32, i32 127, i32 157, i32 353, i32 257, i32 138, i32 266,
	i32 349, i32 151, i32 17, i32 256, i32 242, i32 74, i32 73, i32 15,
	i32 167, i32 84, i32 324, i32 123, i32 278, i32 289, i32 258, i32 356,
	i32 285, i32 34, i32 205, i32 176, i32 117, i32 137, i32 121, i32 105,
	i32 333, i32 365, i32 309, i32 235, i32 253, i32 340, i32 330, i32 53,
	i32 46, i32 28, i32 143, i32 208, i32 145, i32 322, i32 35, i32 356,
	i32 171, i32 314, i32 74, i32 159, i32 1, i32 303, i32 352, i32 345,
	i32 157, i32 12, i32 153, i32 179, i32 149, i32 75, i32 213, i32 102,
	i32 111, i32 250, i32 184, i32 175, i32 234, i32 64, i32 65, i32 312,
	i32 44, i32 252, i32 108, i32 7, i32 230, i32 249, i32 54, i32 245,
	i32 63, i32 330, i32 262, i32 0, i32 20, i32 108, i32 100, i32 61,
	i32 140, i32 187, i32 175, i32 243, i32 183, i32 7, i32 216, i32 345,
	i32 168, i32 49, i32 312, i32 114, i32 139, i32 174, i32 164, i32 79,
	i32 112, i32 187, i32 289, i32 192, i32 17, i32 72, i32 293, i32 88,
	i32 241, i32 86, i32 119, i32 306, i32 196, i32 247, i32 134, i32 151,
	i32 105, i32 11, i32 89, i32 183, i32 31, i32 178, i32 192, i32 358,
	i32 135, i32 350, i32 353, i32 304, i32 189, i32 242, i32 39, i32 368,
	i32 303, i32 185, i32 137, i32 327, i32 329, i32 25, i32 362, i32 72,
	i32 234, i32 274, i32 305, i32 172, i32 226, i32 27, i32 66, i32 87,
	i32 94, i32 112, i32 31, i32 103, i32 277, i32 37, i32 71, i32 235,
	i32 178, i32 319, i32 107, i32 122, i32 249, i32 86, i32 207, i32 85,
	i32 344, i32 92, i32 188, i32 199, i32 201, i32 128, i32 289, i32 306,
	i32 209, i32 364, i32 300, i32 262, i32 305, i32 259, i32 316, i32 194,
	i32 161, i32 129, i32 208, i32 310, i32 297, i32 198, i32 10, i32 48,
	i32 360, i32 90, i32 206, i32 360, i32 148, i32 61, i32 135, i32 148,
	i32 60, i32 207, i32 116, i32 136, i32 83, i32 275, i32 362, i32 157,
	i32 307, i32 141, i32 341, i32 271, i32 81, i32 69, i32 248, i32 135,
	i32 260, i32 241, i32 181, i32 124, i32 364, i32 53, i32 109, i32 129,
	i32 87, i32 23, i32 73, i32 128, i32 31, i32 72, i32 284, i32 343,
	i32 156, i32 23, i32 4, i32 180, i32 168, i32 351, i32 122, i32 342,
	i32 337, i32 113, i32 170, i32 32, i32 3, i32 229, i32 162, i32 367,
	i32 234, i32 308, i32 30, i32 19, i32 283, i32 92, i32 36, i32 206,
	i32 5, i32 314, i32 176, i32 251, i32 325, i32 153, i32 304, i32 202,
	i32 318, i32 258, i32 291, i32 227, i32 238, i32 175, i32 310, i32 366,
	i32 75, i32 62, i32 294, i32 145, i32 255, i32 120, i32 133, i32 296,
	i32 319, i32 224, i32 99, i32 38, i32 243, i32 336, i32 67, i32 26,
	i32 74, i32 77, i32 282, i32 222, i32 24, i32 150, i32 231, i32 349,
	i32 251, i32 132, i32 102, i32 315, i32 56, i32 163, i32 90, i32 60,
	i32 131, i32 45, i32 132, i32 265, i32 143, i32 217, i32 77, i32 260,
	i32 219, i32 283, i32 215, i32 367, i32 197, i32 152, i32 334, i32 182,
	i32 82, i32 361, i32 359, i32 60, i32 95, i32 298, i32 151, i32 340,
	i32 117, i32 210, i32 6, i32 15, i32 73, i32 200, i32 330, i32 144,
	i32 51, i32 69, i32 23, i32 156, i32 125, i32 64, i32 291, i32 111,
	i32 293, i32 281, i32 54, i32 52, i32 267, i32 180, i32 106, i32 134,
	i32 272, i32 282, i32 79, i32 276, i32 196, i32 358, i32 280, i32 128,
	i32 63, i32 150, i32 173
], align 16

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 u0x0000000000000000, ; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

; Functions

; Function attributes: memory(write, argmem: none, inaccessiblemem: none) "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 16

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { memory(write, argmem: none, inaccessiblemem: none) "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!".NET for Android remotes/origin/release/9.0.1xx @ 278e101698269c9bc8840aa94d72e7f24066a96d"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
