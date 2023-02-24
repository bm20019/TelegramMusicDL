    using Telegram.Bot;
    using Telegram.Bot.Exceptions;
    using Telegram.Bot.Polling;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using ConvertAudioLibrary;
    using MusicDownloaderLibrary;
    using System.Net;
    using System.Text.Json;

    namespace TelegramDownloadMusic
    {
       
        public class TelegramBot
        {
            Config config;
            Action<Uri, string> downloadPictureUri = delegate (Uri url, string outputFile)
            {
                WebClient wc = new WebClient();
                wc.DownloadFile(url, outputFile);
            };
            List<string> bitrateList = Enum.GetValues(typeof(Bitrate)).Cast<Bitrate>().Select(v => v.ToString()).ToList();
            List<string> CodecsList = Enum.GetValues(typeof(Codecs)).Cast<Codecs>().Select(v => v.ToString()).ToList();
            public async Task ServiceTelegramDowload(string TokenTelegram)
            {
                var botClient = new TelegramBotClient(TokenTelegram);

                using CancellationTokenSource cts = new(new TimeSpan(0,23,59,59));
                // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
                ReceiverOptions receiverOptions = new()
                {
                    AllowedUpdates = Array.Empty<UpdateType>(), // receive all update types
                    Limit = 100,
                    ThrowPendingUpdates = true
                };
                
                botClient.StartReceiving(
                    updateHandler: HandleUpdateAsync,
                    pollingErrorHandler: HandlePollingErrorAsync,
                    receiverOptions: receiverOptions,
                    cancellationToken: cts.Token
                );
                var me = await botClient.GetMeAsync();

                Console.WriteLine($"Empezar a escuchar @{me.Username}");
                Console.ReadLine();

                // Send cancellation request to stop bot
                cts.Cancel();
            }
            private bool esDigit(string cadena)
            {
                foreach (char item in cadena)
                {
                    if (!char.IsDigit(item))
                        return false;
                }
                return true;
            }
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
              try{
                  Console.WriteLine(update.Message.Text);
              }catch(Exception exe)
              {
                  await ExeceptionTask(exe, botClient, update, cancellationToken);
              }
              // Only process Message updates: https://core.telegram.org/bots/api#message
                if (update.Message is not { } message)
                    return;
                // Only process text messages
                if (message.Text is not { } messageText) 
                    return;
                long chatId = message.Chat.Id;
                string folderID = CreateFolder(chatId);
                string nameFileConfig = Path.Combine(folderID,"config.json");
                //System.IO.File.Exists(Path.Combine(Environment.CurrentDirectory, nameFileConfig))
                if (System.IO.File.Exists(nameFileConfig)==false)
                {
                   System.IO.File.WriteAllText(nameFileConfig, "{\"Bitrate\":128,\"Format\":\"libmp3lame\",\"Template\":\"{trackNumber} {title}\"}");
                }
                string file = System.IO.File.ReadAllText(nameFileConfig);
                config = JsonSerializer.Deserialize<Config>(file); 
                if(messageText.StartsWith("/start")){ 
                    await botClient.SendTextMessageAsync( chatId: chatId, text: "Este bot puede descargar Musica proporcionando una url de spotify, deezer, ytMusic o youtube", cancellationToken: cancellationToken ); 
                    await botClient.SendTextMessageAsync( chatId: chatId, text: "se puede configurar el bitrate y el codec de salida:\n/bitrate 320\n/format libmp3lame\n", cancellationToken: cancellationToken ); 
                    await botClient.SendTextMessageAsync( chatId: chatId, text: "Enviame una url de musica :)", cancellationToken: cancellationToken ); 
                    return; 
                } 
                if (messageText.StartsWith("/bitrate")) 
                { 
                    string[] bitrateArray = messageText.Split(' '); 
                    if (bitrateArray.Length > 1) 
                    { 
                        string bitrateStr = bitrateArray[1];
                        if (esDigit(bitrateStr)) 
                        { 
                            int bitrateInt = Convert.ToInt32(bitrateStr); 
                          if (bitrateList.Contains("btr_" + bitrateStr)) 
                          { 
                              config.Bitrate = bitrateInt;
                              System.IO.File.WriteAllText("config.json", JsonSerializer.Serialize(config));
                              Console.WriteLine("Se Cambio el bitrate");
                              await botClient.SendTextMessageAsync(
                                  chatId: chatId, 
                                  text: "Bitrate Cambiado a: " + bitrateStr, 
                                  cancellationToken: cancellationToken
                                  ); 
                          }
                          else 
                          { 
                              config.Bitrate = 128; 
                              System.IO.File.WriteAllText("config.json", JsonSerializer.Serialize(config)); 
                              Console.WriteLine("Se Cambio el bitrate");
                              Message sentMessageText = await botClient.SendTextMessageAsync(
                                  chatId: chatId, 
                                  text: "Bitrate Cambiado a: " + 128, 
                                  cancellationToken: cancellationToken
                                  ); 
                          } 
                      } 
                  }
                  return; 
              }else if (messageText.StartsWith("/format")) 
              {
                    string[] formatArray = messageText.Split(' ');
                    if (formatArray.Length > 1)
                    {
                        string formatStr = formatArray[1];
                        if (CodecsList.Contains(formatStr)) 
                        { 
                            config.Format = formatStr;
                            System.IO.File.WriteAllText(nameFileConfig, JsonSerializer.Serialize(config));
                                Console.WriteLine("Se Cambio el Codec");
                                Message sentMessageText = await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "Bitrate Cambiado a: " + formatStr,
                                    cancellationToken: cancellationToken
                                ); 
                        }
                        else 
                        {
                            config.Format = "libmp3lame"; 
                            System.IO.File.WriteAllText(nameFileConfig, JsonSerializer.Serialize(config)); 
                            Console.WriteLine("Se Cambio el Codec"); 
                            Message sentMessageText = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Codec cambiado a: " + "libmp3lame",
                                cancellationToken: cancellationToken
                                ); 
                        } 
                    }
                    return;
                }

                if (Utils.UrlServerIs(messageText))
                {
                    Download dl = new Download();
                    string pathIDFoler = folderID;//Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),""+update.Message.Chat.Id);
                    Bitrate bit = Bitrate.btr_320;
                    Codecs codecs = Codecs.libmp3lame; 
                    if (config != null)
                    {
                        Console.WriteLine("Codecs: "+config.Format);
                        bit = (Bitrate)Enum.Parse(typeof(Bitrate), "btr_" + config.Bitrate);
                        codecs = (Codecs)Enum.Parse(typeof(Codecs), config.Format);
                    } 
                    Metadata datainfo;
                    string photoFile = "";
                  try{ 
                      datainfo = dl.GetDataInfo(messageText);
                  }catch(Exception ex){ 
                      await ExeceptionTask(ex,botClient , update, cancellationToken); 
                      return;
                  } 
                  try 
                  { 
                      photoFile = Path.Combine(pathIDFoler, datainfo.id + ".jpeg"); 
                      downloadPictureUri(new Uri(dl.GetCovertArtMax()), photoFile); 
                      InputFile foto = new InputFile(System.IO.File.OpenRead(photoFile), Guid.NewGuid().ToString() + ".jpeg");
                        try
                        {
                            //Codecs.libmp3lame
                            Console.WriteLine("Enviando CoverArt");
                            await botClient.SendPhotoAsync(
                                chatId: chatId, 
                                photo: foto, 
                                caption: $"Titulo: {datainfo.Title}\nAlbum: {datainfo.Album}\nArtista:{datainfo.Performers[0]}\nAño: {datainfo.Year}", 
                                cancellationToken: cancellationToken
                                );
                            
                            //Descargando Audio
                            await dl.MusicDownload(messageText, new DirectoryInfo(pathIDFoler), bit, codecs);
                            string fd = dl.PathFileOutput();
                            await using FileStream stream = System.IO.File.OpenRead(fd);
                            InputFile musicDownload = new InputFile(stream, new FileInfo(fd).Name);
                            try
                            { 
                                Console.WriteLine("Enviando Audio...");
                                await botClient.SendAudioAsync(
                                    chatId: chatId, 
                                    audio: musicDownload, 
                                    cancellationToken: cancellationToken
                            );
                                Console.WriteLine("Eliminando Datos...");
                                if(System.IO.File.Exists(fd))
                                    System.IO.File.Delete(fd);
                                if(System.IO.File.Exists(photoFile))
                                    System.IO.File.Delete(photoFile);
                                Console.WriteLine("Operacion Completada");
                            }
                            catch (Exception ex)
                            {
                                await ExeceptionTask(ex,botClient , update, cancellationToken);
                                if(System.IO.File.Exists(fd))
                                    System.IO.File.Delete(fd);
                                if(System.IO.File.Exists(photoFile))
                                    System.IO.File.Delete(photoFile);
                            }
                        }
                        catch (Exception ex)
                        {
                            await ExeceptionTask(ex,botClient , update, cancellationToken);
                        }
                  }
                  catch (Exception ex)
                  {
                      await ExeceptionTask(ex,botClient , update, cancellationToken);
                  }
                }
                else
                { 
                    await botClient.SendTextMessageAsync(
                        chatId: chatId, 
                        text: "Unsupported url :(", 
                        cancellationToken: cancellationToken
                        );
                }
            }

            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var errorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                Console.WriteLine(errorMessage);
                return Task.CompletedTask;
            }

            private static async Task ExeceptionTask(Exception ex, ITelegramBotClient botClient,Update update, CancellationToken cancellationToken)
            {
                string da=""; 
                foreach (string item in ex.Data) 
                { 
                    da += item+"\n"; 
                }
                try
                {
                    await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id, 
                    text: ex.Message+"\n"+da, 
                    cancellationToken: cancellationToken
                ); 
                Console.WriteLine($"{ex.Message}\n{ex.Source}\n{ex.GetBaseException()}"); 
                }
                catch (System.Exception e)
                {
                    Console.WriteLine($"{e.Message}\n{e.Source}\n{e.GetBaseException()}"); 
                   Console.WriteLine("No se pudo enviar el Mensaje");
                }
                
            }
            private static string CreateFolder(long id){
                var chatFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),""+id);
                Directory.CreateDirectory(chatFolder);
                Directory.SetCurrentDirectory(chatFolder);
                return chatFolder;
            }
        }
    }