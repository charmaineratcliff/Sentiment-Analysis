# Sentiment Analysis

This was a great project from my MIS3673 class at ECU during the Spring 2018 semester.  It was a project that required we develop a C# Windows-based application that performed a basic sentiment analysis.  

Generally speaking, the analysis aimed to determine the attitude of the speaker, writer, or other subject with respect to a topic or the overall contextual polarity or emotional reaction to a target.  

This application is able to read text from the input in the textbox or from a file.  Once imported, the words from the text are compared to dictionaries of negative and positive words.  After all of the words are compared, two scores, a negativity score and a positivity score are obtained.  

The sentiment score is calculated by using this formula: S=(P-N)/(P+N).  

A negative sentiment value is indicated by a negative one, zero means neutral and a positive value is indicated by a positive one.

## Getting Started

1. To run the application, clone or download the repo.
2. Run the application from the bin/Debug folder > Final Project.exe
