using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

namespace SBR
{
    public class DataHandler : Singleton<DataHandler>
    {
        protected DataHandler() {}
        private string saveFileName = "savedata.json";
        public string filePath => Path.Join(Application.persistentDataPath, saveFileName);
        public SaveStructure Load()
        {
            string path = filePath;
            if(!File.Exists(path)) return null;
            
            string FromJsonData = File.ReadAllText(filePath);
            try {
                return JsonUtility.FromJson<SaveStructure>(FromJsonData);
            } catch {
                return null;
            }
        }
        public void RemoveSaveFile() => File.Delete(filePath);
        
        public void Save(SaveStructure data) => StartCoroutine(SaveSync(data));
        
        private IEnumerator SaveSync(SaveStructure data)
        {
            yield return null;
            try {
                string json = JsonUtility.ToJson(data, true);

                File.WriteAllText(filePath, json);
            } catch (Exception e){
                Debug.LogError(e.StackTrace);
            }
        }

        private string highScoreSaveFileName = "highscoredata.json";
        public string highScoreFilePath => Path.Join(Application.persistentDataPath, highScoreSaveFileName);
        public int LoadHighScore()
        {
            string path = highScoreFilePath;
            if (!File.Exists(path)) return 1;

            string FromJsonData = File.ReadAllText(highScoreFilePath);
            try
            {
                return JsonUtility.FromJson<HighScoreInfo>(FromJsonData).highScore;
            }
            catch
            {
                return 1;
            }
        }

        public void SaveHighScore(int data) => StartCoroutine(SaveHighScoreSync(data));

        private IEnumerator SaveHighScoreSync(int data)
        {
            yield return null;
            try
            {
                HighScoreInfo h = new HighScoreInfo(data);
                string json = JsonUtility.ToJson(h, true);

                File.WriteAllText(highScoreFilePath, json);
            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }
        }
    }
}